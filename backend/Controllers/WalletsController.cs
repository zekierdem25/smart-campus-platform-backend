using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using System.Security.Claims;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/wallet")]
[Authorize]
public class WalletsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IPaymentService _paymentService;
    private readonly ILogger<WalletsController> _logger;

    public WalletsController(
        ApplicationDbContext context,
        IPaymentService paymentService,
        ILogger<WalletsController> logger)
    {
        _context = context;
        _paymentService = paymentService;
        _logger = logger;
    }

    /// <summary>
    /// Get current user's wallet balance
    /// </summary>
    [HttpGet("balance")]
    public async Task<ActionResult<object>> GetBalance()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var wallet = await _context.Wallets
            .FirstOrDefaultAsync(w => w.UserId == userId);

        if (wallet == null)
        {
            // Create wallet if doesn't exist
            wallet = new Wallet { UserId = userId };
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
        }

        return Ok(new
        {
            wallet.Id,
            wallet.Balance,
            wallet.Currency,
            wallet.IsActive,
            wallet.UpdatedAt
        });
    }

    /// <summary>
    /// Initiate wallet top-up via payment gateway
    /// Returns a payment URL for the user to complete payment
    /// </summary>
    [HttpPost("topup")]
    public async Task<ActionResult<object>> TopUp([FromBody] TopUpDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        if (dto.Amount < 50)
            return BadRequest(new { message = "Minimum yükleme tutarı 50 TL'dir" });

        if (dto.Amount > 5000)
            return BadRequest(new { message = "Maksimum yükleme tutarı 5000 TL'dir" });

        // Ensure wallet exists
        var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
        if (wallet == null)
        {
            wallet = new Wallet { UserId = userId };
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
        }

        _logger.LogInformation("User {UserId} initiating top-up of {Amount} TL", userId, dto.Amount);

        // Create payment session via PaymentService
        var result = await _paymentService.CreatePaymentSessionAsync(dto.Amount, userId, "TRY");

        if (!result.Success)
        {
            return BadRequest(new { message = result.ErrorMessage ?? "Ödeme oturumu oluşturulamadı" });
        }

        return Ok(new
        {
            paymentUrl = result.PaymentUrl,
            sessionId = result.SessionId,
            message = "Ödeme sayfasına yönlendiriliyorsunuz"
        });
    }

    /// <summary>
    /// Payment webhook callback for processing completed payments
    /// Called by the payment gateway when payment is completed
    /// </summary>
    [HttpPost("topup/webhook")]
    [AllowAnonymous]
    public async Task<IActionResult> TopUpWebhook([FromBody] PaymentWebhookDto dto)
    {
        _logger.LogInformation("Payment webhook received: PaymentId={PaymentId}, Status={Status}", 
            dto.PaymentId, dto.Status);

        // Get signature from header or DTO for verification
        var signature = Request.Headers["X-Webhook-Signature"].FirstOrDefault() ?? dto.Signature ?? "";

        // Create payload from DTO for signature verification
        // In production, you'd use EnableBuffering middleware and read raw body
        var payload = System.Text.Json.JsonSerializer.Serialize(dto);

        // Verify webhook signature
        var isValid = await _paymentService.VerifyWebhookSignatureAsync(payload, signature);
        if (!isValid)
        {
            _logger.LogWarning("Invalid webhook signature for payment {PaymentId}", dto.PaymentId);
            return Unauthorized(new { message = "Geçersiz webhook imzası" });
        }

        // Validate required fields
        if (string.IsNullOrEmpty(dto.PaymentId) || !dto.UserId.HasValue)
        {
            return BadRequest(new { message = "Eksik ödeme bilgisi" });
        }

        // Process the payment
        var result = await _paymentService.ProcessPaymentWebhookAsync(
            dto.PaymentId, 
            dto.Status, 
            dto.Amount, 
            dto.UserId.Value);

        if (!result.Success)
        {
            _logger.LogWarning("Payment processing failed: {PaymentId}, Error: {Error}", 
                dto.PaymentId, result.ErrorMessage);
            return BadRequest(new { message = result.ErrorMessage });
        }

        _logger.LogInformation("Payment processed successfully: {PaymentId}, TransactionId={TransactionId}", 
            dto.PaymentId, result.TransactionId);

        return Ok(new 
        { 
            message = "Ödeme başarıyla işlendi",
            transactionId = result.TransactionId,
            amount = result.Amount
        });
    }

    /// <summary>
    /// Simulate payment completion (for testing purposes only)
    /// This endpoint should be disabled in production
    /// </summary>
    [HttpPost("topup/simulate")]
    public async Task<ActionResult<object>> SimulatePayment([FromBody] SimulatePaymentDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        _logger.LogInformation("Simulating payment completion for session {SessionId}", dto.SessionId);

        // Find the pending payment
        var pendingPayment = await _context.PendingPayments
            .FirstOrDefaultAsync(p => p.SessionId == dto.SessionId && p.UserId == userId);

        if (pendingPayment == null)
        {
            return NotFound(new { message = "Ödeme oturumu bulunamadı" });
        }

        if (pendingPayment.Status == "completed")
        {
            return BadRequest(new { message = "Bu ödeme zaten tamamlanmış" });
        }

        // Process the payment as if webhook was received
        var result = await _paymentService.ProcessPaymentWebhookAsync(
            pendingPayment.SessionId,
            "success",
            pendingPayment.Amount,
            userId);

        if (!result.Success)
        {
            return BadRequest(new { message = result.ErrorMessage });
        }

        // Get updated wallet balance
        var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);

        return Ok(new
        {
            message = "Ödeme simülasyonu başarılı",
            transactionId = result.TransactionId,
            amount = result.Amount,
            newBalance = wallet?.Balance ?? 0
        });
    }

    /// <summary>
    /// Get transaction history
    /// </summary>
    [HttpGet("transactions")]
    public async Task<ActionResult<object>> GetTransactions(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] TransactionType? type = null,
        [FromQuery] DateTime? dateFrom = null,
        [FromQuery] DateTime? dateTo = null)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
        if (wallet == null)
            return Ok(new { transactions = new List<object>(), totalCount = 0 });

        var query = _context.Transactions
            .Where(t => t.WalletId == wallet.Id)
            .AsQueryable();

        if (type.HasValue)
            query = query.Where(t => t.Type == type.Value);

        if (dateFrom.HasValue)
            query = query.Where(t => t.CreatedAt >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(t => t.CreatedAt <= dateTo.Value);

        var totalCount = await query.CountAsync();

        var transactions = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new
            {
                t.Id,
                t.Type,
                t.Amount,
                t.BalanceAfter,
                t.ReferenceType,
                t.ReferenceId,
                t.Description,
                t.CreatedAt
            })
            .ToListAsync();

        return Ok(new
        {
            transactions,
            totalCount,
            page,
            pageSize,
            totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        });
    }

    /// <summary>
    /// Get pending payments for current user
    /// </summary>
    [HttpGet("pending-payments")]
    public async Task<ActionResult<object>> GetPendingPayments()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var pendingPayments = await _context.PendingPayments
            .Where(p => p.UserId == userId && p.Status == "pending" && p.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new
            {
                p.SessionId,
                p.Amount,
                p.Currency,
                p.Status,
                p.Provider,
                p.CreatedAt,
                p.ExpiresAt
            })
            .ToListAsync();

        return Ok(pendingPayments);
    }
}

// DTOs
public record TopUpDto(decimal Amount);

public record PaymentWebhookDto(
    string PaymentId,
    string Status,
    decimal Amount,
    Guid? UserId,
    string? Signature
);

public record SimulatePaymentDto(string SessionId);
