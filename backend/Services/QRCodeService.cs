using System.Text;
using System.Text.Json;

namespace SmartCampus.API.Services;

/// <summary>
/// Interface for unified QR code generation and validation
/// </summary>
public interface IQRCodeService
{
    /// <summary>
    /// Generates a unified format QR code with prefix and optional data
    /// </summary>
    string GenerateQRCode(string prefix, Dictionary<string, object>? data = null);

    /// <summary>
    /// Generates a simple QR code with just prefix and GUID (legacy compatible)
    /// </summary>
    string GenerateSimpleQRCode(string prefix);

    /// <summary>
    /// Validates a QR code against expected prefix
    /// </summary>
    bool ValidateQRCode(string qrCode, string expectedPrefix);

    /// <summary>
    /// Parses QR code and extracts data
    /// </summary>
    QRCodeData? ParseQRCode(string qrCode);

    /// <summary>
    /// Checks if QR code is in legacy format (PREFIX-GUID)
    /// </summary>
    bool IsLegacyFormat(string qrCode);
}

/// <summary>
/// Parsed QR code data
/// </summary>
public class QRCodeData
{
    public string Prefix { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public Dictionary<string, object> Data { get; set; } = new();
    public bool IsLegacy { get; set; }
}

/// <summary>
/// Service for unified QR code generation, validation, and parsing
/// Supports both legacy format (PREFIX-GUID) and new unified format (BASE64 JSON)
/// </summary>
public class QRCodeService : IQRCodeService
{
    private readonly ILogger<QRCodeService> _logger;

    // Known prefixes for validation
    private static readonly HashSet<string> KnownPrefixes = new()
    {
        "MEAL",
        "EVENT",
        "ATTENDANCE",
        "EQUIPMENT"
    };

    public QRCodeService(ILogger<QRCodeService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Generates a unified format QR code
    /// Format: BASE64({prefix}:{json_data})
    /// </summary>
    public string GenerateQRCode(string prefix, Dictionary<string, object>? data = null)
    {
        var payload = new Dictionary<string, object>
        {
            ["prefix"] = prefix.ToUpperInvariant(),
            ["timestamp"] = DateTime.UtcNow.ToString("o"),
            ["id"] = Guid.NewGuid().ToString("N"),
            ["data"] = data ?? new Dictionary<string, object>()
        };

        var json = JsonSerializer.Serialize(payload);
        var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        
        _logger.LogDebug("Generated QR code with prefix: {Prefix}", prefix);
        return base64;
    }

    /// <summary>
    /// Generates a simple legacy-compatible QR code
    /// Format: PREFIX-GUID (e.g., MEAL-abc123def456...)
    /// </summary>
    public string GenerateSimpleQRCode(string prefix)
    {
        var code = $"{prefix.ToUpperInvariant()}-{Guid.NewGuid():N}";
        _logger.LogDebug("Generated simple QR code: {Code}", code);
        return code;
    }

    /// <summary>
    /// Validates a QR code against expected prefix
    /// Supports both legacy and new formats
    /// </summary>
    public bool ValidateQRCode(string qrCode, string expectedPrefix)
    {
        if (string.IsNullOrEmpty(qrCode))
            return false;

        try
        {
            // Check legacy format first (PREFIX-GUID)
            if (IsLegacyFormat(qrCode))
            {
                return qrCode.StartsWith($"{expectedPrefix.ToUpperInvariant()}-", StringComparison.OrdinalIgnoreCase);
            }

            // Try new format (BASE64 JSON)
            var parsed = ParseQRCode(qrCode);
            if (parsed == null)
                return false;

            return parsed.Prefix.Equals(expectedPrefix, StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to validate QR code");
            return false;
        }
    }

    /// <summary>
    /// Parses QR code and extracts data
    /// Handles both legacy and new formats
    /// </summary>
    public QRCodeData? ParseQRCode(string qrCode)
    {
        if (string.IsNullOrEmpty(qrCode))
            return null;

        try
        {
            // Check legacy format (PREFIX-GUID)
            if (IsLegacyFormat(qrCode))
            {
                var parts = qrCode.Split('-', 2);
                if (parts.Length != 2)
                    return null;

                return new QRCodeData
                {
                    Prefix = parts[0].ToUpperInvariant(),
                    Timestamp = DateTime.MinValue, // Unknown for legacy
                    Data = new Dictionary<string, object>
                    {
                        ["guid"] = parts[1]
                    },
                    IsLegacy = true
                };
            }

            // Parse new format (BASE64 JSON)
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(qrCode));
            var payload = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);

            if (payload == null || !payload.ContainsKey("prefix"))
                return null;

            var result = new QRCodeData
            {
                Prefix = payload["prefix"].GetString() ?? string.Empty,
                IsLegacy = false
            };

            if (payload.TryGetValue("timestamp", out var timestampElement))
            {
                if (DateTime.TryParse(timestampElement.GetString(), out var timestamp))
                    result.Timestamp = timestamp;
            }

            if (payload.TryGetValue("data", out var dataElement))
            {
                result.Data = JsonSerializer.Deserialize<Dictionary<string, object>>(dataElement.GetRawText()) 
                    ?? new Dictionary<string, object>();
            }

            if (payload.TryGetValue("id", out var idElement))
            {
                result.Data["id"] = idElement.GetString() ?? string.Empty;
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to parse QR code: {QRCode}", qrCode.Length > 20 ? qrCode[..20] + "..." : qrCode);
            return null;
        }
    }

    /// <summary>
    /// Checks if QR code is in legacy format (PREFIX-GUID)
    /// Legacy format: PREFIX-32HEXCHARS (e.g., MEAL-abc123def456789012345678901234)
    /// </summary>
    public bool IsLegacyFormat(string qrCode)
    {
        if (string.IsNullOrEmpty(qrCode))
            return false;

        // Legacy format check: PREFIX-GUID format
        foreach (var prefix in KnownPrefixes)
        {
            if (qrCode.StartsWith($"{prefix}-", StringComparison.OrdinalIgnoreCase))
            {
                // Check if the rest looks like a GUID (lowercase hex, 32 chars)
                var guidPart = qrCode[(prefix.Length + 1)..];
                if (guidPart.Length >= 32 && guidPart.All(c => char.IsLetterOrDigit(c)))
                    return true;
            }
        }

        // Also check for any UPPERCASE-GUID pattern
        var dashIndex = qrCode.IndexOf('-');
        if (dashIndex > 0 && dashIndex < 20)
        {
            var prefix = qrCode[..dashIndex];
            var guid = qrCode[(dashIndex + 1)..];
            
            // Legacy format: prefix is uppercase letters only, guid is hex
            return prefix.All(char.IsUpper) && 
                   guid.Length >= 32 && 
                   guid.All(c => char.IsLetterOrDigit(c));
        }

        return false;
    }
}
