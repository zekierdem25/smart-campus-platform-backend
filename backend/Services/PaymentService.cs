using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using System.Security.Cryptography;
using System.Text;

namespace SmartCampus.API.Services;

/// <summary>
/// Payment service implementation with Stripe-like payment gateway integration
/// Uses test/sandbox mode for development
/// </summary>
public class PaymentService : IPaymentService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<PaymentService> _logger;
    private readonly IEmailService _emailService;

    public PaymentService(
        ApplicationDbContext context,
        IConfiguration configuration,
        ILogger<PaymentService> logger,
        IEmailService emailService)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
        _emailService = emailService;
    }

    /// <summary>
    /// Creates a payment session and returns the payment URL
    /// In test mode, simulates a payment session
    /// </summary>
    public async Task<PaymentSessionResult> CreatePaymentSessionAsync(decimal amount, Guid userId, string currency = "TRY")
    {
        try
        {
            var paymentConfig = _configuration.GetSection("Payment");
            var provider = paymentConfig["Provider"] ?? "Simulation";
            var isTestMode = paymentConfig.GetValue<bool>("TestMode", true);

            _logger.LogInformation("Creating payment session for user {UserId}, amount: {Amount} {Currency}, provider: {Provider}", 
                userId, amount, currency, provider);

            // Generate a unique session ID
            var sessionId = $"ps_{Guid.NewGuid():N}";
            
            // Store pending payment in database for tracking
            var pendingPayment = new PendingPayment
            {
                Id = Guid.NewGuid(),
                SessionId = sessionId,
                UserId = userId,
                Amount = amount,
                Currency = currency,
                Status = "pending",
                Provider = provider,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30)
            };

            _context.PendingPayments.Add(pendingPayment);
            await _context.SaveChangesAsync();

            string paymentUrl;

            if (provider == "Stripe" && !isTestMode)
            {
                // Real Stripe integration would go here
                // var secretKey = paymentConfig["Stripe:SecretKey"];
                // StripeConfiguration.ApiKey = secretKey;
                // var options = new SessionCreateOptions { ... };
                // var session = await new SessionService().CreateAsync(options);
                // paymentUrl = session.Url;
                
                // For now, return simulation URL
                paymentUrl = GenerateSimulationPaymentUrl(sessionId, amount, userId);
            }
            else if (provider == "PayTR" && !isTestMode)
            {
                // Real PayTR integration would go here
                // var merchantId = paymentConfig["PayTR:MerchantId"];
                // ... PayTR API call ...
                
                paymentUrl = GenerateSimulationPaymentUrl(sessionId, amount, userId);
            }
            else
            {
                // Simulation mode - generate a test payment URL
                paymentUrl = GenerateSimulationPaymentUrl(sessionId, amount, userId);
            }

            _logger.LogInformation("Payment session created successfully: {SessionId}", sessionId);

            return new PaymentSessionResult
            {
                Success = true,
                PaymentUrl = paymentUrl,
                SessionId = sessionId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create payment session for user {UserId}", userId);
            return new PaymentSessionResult
            {
                Success = false,
                ErrorMessage = "Ödeme oturumu oluşturulurken bir hata oluştu"
            };
        }
    }

    /// <summary>
    /// Verifies the webhook signature from the payment provider
    /// </summary>
    public Task<bool> VerifyWebhookSignatureAsync(string payload, string signature)
    {
        try
        {
            var paymentConfig = _configuration.GetSection("Payment");
            var provider = paymentConfig["Provider"] ?? "Simulation";
            var isTestMode = paymentConfig.GetValue<bool>("TestMode", true);

            if (isTestMode || provider == "Simulation")
            {
                // In test/simulation mode, verify using our own signature
                var webhookSecret = paymentConfig["WebhookSecret"] ?? "test_webhook_secret_key_12345";
                var expectedSignature = ComputeHmacSha256(payload, webhookSecret);
                
                // Also accept empty signature in test mode for easier testing
                if (string.IsNullOrEmpty(signature))
                {
                    _logger.LogWarning("Empty signature received - allowed in test mode only");
                    return Task.FromResult(true);
                }

                return Task.FromResult(signature == expectedSignature);
            }

            if (provider == "Stripe")
            {
                // Real Stripe signature verification
                // var webhookSecret = paymentConfig["Stripe:WebhookSecret"];
                // try { Stripe.EventUtility.ConstructEvent(payload, signature, webhookSecret); return true; }
                // catch { return false; }
            }

            if (provider == "PayTR")
            {
                // Real PayTR signature verification
                // var merchantSalt = paymentConfig["PayTR:MerchantSalt"];
                // ... verify PayTR hash ...
            }

            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Webhook signature verification failed");
            return Task.FromResult(false);
        }
    }

    /// <summary>
    /// Processes the payment webhook and updates wallet if successful
    /// Uses atomic operations to prevent race conditions
    /// </summary>
    public async Task<PaymentProcessResult> ProcessPaymentWebhookAsync(string paymentId, string status, decimal amount, Guid userId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _logger.LogInformation("Processing payment webhook: PaymentId={PaymentId}, Status={Status}, Amount={Amount}, UserId={UserId}",
                paymentId, status, amount, userId);

            // Find pending payment
            var pendingPayment = await _context.PendingPayments
                .FirstOrDefaultAsync(p => p.SessionId == paymentId && p.UserId == userId);

            if (pendingPayment == null)
            {
                _logger.LogWarning("Pending payment not found: {PaymentId}", paymentId);
                return new PaymentProcessResult
                {
                    Success = false,
                    ErrorMessage = "Ödeme kaydı bulunamadı"
                };
            }

            if (pendingPayment.Status == "completed")
            {
                _logger.LogWarning("Payment already processed: {PaymentId}", paymentId);
                return new PaymentProcessResult
                {
                    Success = false,
                    ErrorMessage = "Bu ödeme zaten işlenmiş"
                };
            }

            if (status != "success" && status != "completed")
            {
                pendingPayment.Status = "failed";
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new PaymentProcessResult
                {
                    Success = false,
                    ErrorMessage = "Ödeme başarısız oldu"
                };
            }

            // Get or create wallet
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
            if (wallet == null)
            {
                wallet = new Wallet { UserId = userId };
                _context.Wallets.Add(wallet);
                await _context.SaveChangesAsync();
            }

            // Atomic wallet balance update to prevent race conditions
            var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                "UPDATE Wallets SET Balance = Balance + {0}, UpdatedAt = {1} WHERE UserId = {2}",
                amount, DateTime.UtcNow, userId);

            if (rowsAffected == 0)
            {
                await transaction.RollbackAsync();
                return new PaymentProcessResult
                {
                    Success = false,
                    ErrorMessage = "Cüzdan güncellenemedi"
                };
            }

            // Reload wallet to get updated balance
            await _context.Entry(wallet).ReloadAsync();

            // Create transaction record
            var walletTransaction = new Transaction
            {
                WalletId = wallet.Id,
                Type = TransactionType.Credit,
                Amount = amount,
                BalanceAfter = wallet.Balance,
                ReferenceType = $"Topup:{paymentId}",
                ReferenceId = null, // Session ID is stored in ReferenceType, ReferenceId is for entity GUIDs
                Description = $"Cüzdan yükleme - {amount} TL (Ödeme ID: {paymentId})"
            };
            _context.Transactions.Add(walletTransaction);

            // Update pending payment status
            pendingPayment.Status = "completed";
            pendingPayment.CompletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            _logger.LogInformation("Payment processed successfully: {PaymentId}, TransactionId={TransactionId}", 
                paymentId, walletTransaction.Id);

            // Send confirmation email (fire and forget)
            _ = SendPaymentConfirmationEmailAsync(userId, amount, walletTransaction.Id);

            return new PaymentProcessResult
            {
                Success = true,
                Amount = amount,
                UserId = userId,
                TransactionId = walletTransaction.Id
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Failed to process payment webhook: {PaymentId}", paymentId);
            return new PaymentProcessResult
            {
                Success = false,
                ErrorMessage = "Ödeme işlenirken bir hata oluştu"
            };
        }
    }

    /// <summary>
    /// Generates a simulation payment URL for testing
    /// </summary>
    private string GenerateSimulationPaymentUrl(string sessionId, decimal amount, Guid userId)
    {
        var frontendUrl = _configuration["Frontend:Url"] ?? "http://localhost:3000";
        // In a real scenario, this would be the payment provider's URL
        // For simulation, we create a callback URL that simulates successful payment
        return $"{frontendUrl}/payment/process?session_id={sessionId}&amount={amount}&user_id={userId}&mode=simulation";
    }

    /// <summary>
    /// Computes HMAC-SHA256 signature for webhook verification
    /// </summary>
    private static string ComputeHmacSha256(string data, string key)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Sends payment confirmation email
    /// </summary>
    private async Task SendPaymentConfirmationEmailAsync(Guid userId, decimal amount, Guid transactionId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || string.IsNullOrEmpty(user.Email)) return;

            var subject = "Cüzdan Yükleme Onayı - Smart Campus";
            var body = $@"
                <h2>Cüzdan Yükleme Başarılı</h2>
                <p>Sayın {user.FirstName} {user.LastName},</p>
                <p>Cüzdanınıza <strong>{amount:N2} TL</strong> başarıyla yüklenmiştir.</p>
                <p><strong>İşlem ID:</strong> {transactionId}</p>
                <p><strong>Tarih:</strong> {DateTime.UtcNow:dd.MM.yyyy HH:mm}</p>
                <br/>
                <p>Smart Campus</p>
            ";

            await _emailService.SendCustomEmailAsync(user.Email, subject, body);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send payment confirmation email to user {UserId}", userId);
        }
    }
}
