namespace SmartCampus.API.Services;

/// <summary>
/// Payment service interface for payment gateway integration
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Creates a payment session and returns the payment URL
    /// </summary>
    Task<PaymentSessionResult> CreatePaymentSessionAsync(decimal amount, Guid userId, string currency = "TRY");
    
    /// <summary>
    /// Verifies the webhook signature from the payment provider
    /// </summary>
    Task<bool> VerifyWebhookSignatureAsync(string payload, string signature);
    
    /// <summary>
    /// Processes the payment webhook and updates wallet if successful
    /// </summary>
    Task<PaymentProcessResult> ProcessPaymentWebhookAsync(string paymentId, string status, decimal amount, Guid userId);
}

/// <summary>
/// Result of creating a payment session
/// </summary>
public class PaymentSessionResult
{
    public bool Success { get; set; }
    public string? PaymentUrl { get; set; }
    public string? SessionId { get; set; }
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Result of processing a payment webhook
/// </summary>
public class PaymentProcessResult
{
    public bool Success { get; set; }
    public decimal Amount { get; set; }
    public Guid UserId { get; set; }
    public Guid? TransactionId { get; set; }
    public string? ErrorMessage { get; set; }
}
