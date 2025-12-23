using System.Text.RegularExpressions;

namespace SmartCampus.API.Utils;

/// <summary>
/// Utility class for input sanitization and validation
/// </summary>
public static class InputSanitizer
{
    /// <summary>
    /// Sanitize HTML input to prevent XSS attacks
    /// </summary>
    public static string SanitizeHtml(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // Remove potentially dangerous HTML tags
        var dangerousTags = new[] { "script", "iframe", "object", "embed", "link", "style", "meta" };
        var pattern = $@"<({string.Join("|", dangerousTags)})(\s[^>]*)?>.*?</\1>|<({string.Join("|", dangerousTags)})(\s[^>]*)?/?>";
        
        input = Regex.Replace(input, pattern, "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        
        // Remove javascript: and data: protocols
        input = Regex.Replace(input, @"javascript:", "", RegexOptions.IgnoreCase);
        input = Regex.Replace(input, @"data:", "", RegexOptions.IgnoreCase);
        
        return input;
    }

    /// <summary>
    /// Sanitize SQL input (EF Core already handles parameterized queries, but this is extra safety)
    /// </summary>
    public static string SanitizeSql(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // Remove SQL injection patterns
        var dangerousPatterns = new[]
        {
            @"(\b(SELECT|INSERT|UPDATE|DELETE|DROP|CREATE|ALTER|EXEC|EXECUTE|UNION|SCRIPT)\b)",
            @"(--|;|\*|')",
            @"(/\*|\*/)"
        };

        foreach (var pattern in dangerousPatterns)
        {
            input = Regex.Replace(input, pattern, "", RegexOptions.IgnoreCase);
        }

        return input;
    }

    /// <summary>
    /// Validate and sanitize email address
    /// </summary>
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            return emailRegex.IsMatch(email) && email.Length <= 255;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Validate and sanitize URL
    /// </summary>
    public static bool IsValidUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;

        return Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
               (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }

    /// <summary>
    /// Sanitize file name to prevent path traversal
    /// </summary>
    public static string SanitizeFileName(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return fileName;

        // Remove path traversal characters
        fileName = fileName.Replace("..", "");
        fileName = fileName.Replace("/", "");
        fileName = fileName.Replace("\\", "");
        fileName = fileName.Replace(":", "");
        fileName = fileName.Replace("*", "");
        fileName = fileName.Replace("?", "");
        fileName = fileName.Replace("\"", "");
        fileName = fileName.Replace("<", "");
        fileName = fileName.Replace(">", "");
        fileName = fileName.Replace("|", "");

        // Limit length
        if (fileName.Length > 255)
        {
            var extension = Path.GetExtension(fileName);
            var nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            fileName = nameWithoutExt.Substring(0, Math.Min(255 - extension.Length, nameWithoutExt.Length)) + extension;
        }

        return fileName;
    }
}

