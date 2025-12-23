using System.Collections.Concurrent;
using System.Net;

namespace SmartCampus.API.Middleware;

/// <summary>
/// Simple in-memory rate limiting middleware
/// For production, consider using AspNetCoreRateLimit or distributed cache
/// </summary>
public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RateLimitingMiddleware> _logger;
    private readonly ConcurrentDictionary<string, RateLimitInfo> _rateLimitStore = new();
    private readonly TimeSpan _window = TimeSpan.FromMinutes(1);
    private readonly int _maxRequests = 60; // 60 requests per minute per IP
    private readonly int _maxLoginRequests = 5; // 5 login attempts per minute per IP

    public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = GetClientIpAddress(context);
        var path = context.Request.Path.Value?.ToLower() ?? "";

        // Skip rate limiting for certain paths
        if (ShouldSkipRateLimit(path))
        {
            await _next(context);
            return;
        }

        // Check rate limit
        var isLoginEndpoint = path.Contains("/auth/login");
        var maxRequests = isLoginEndpoint ? _maxLoginRequests : _maxRequests;
        var key = $"{ipAddress}:{path}";

        if (!IsWithinRateLimit(key, maxRequests))
        {
            _logger.LogWarning("Rate limit exceeded for IP {IpAddress} on path {Path}", ipAddress, path);
            context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
            {
                message = "Çok fazla istek gönderildi. Lütfen bir süre sonra tekrar deneyin.",
                retryAfter = 60
            }));
            return;
        }

        await _next(context);
    }

    private bool ShouldSkipRateLimit(string path)
    {
        // Skip rate limiting for health checks, swagger, static files
        return path.StartsWith("/swagger") ||
               path.StartsWith("/hangfire") ||
               path.StartsWith("/uploads") ||
               path == "/health" ||
               path == "/";
    }

    private bool IsWithinRateLimit(string key, int maxRequests)
    {
        var now = DateTime.UtcNow;
        var info = _rateLimitStore.GetOrAdd(key, _ => new RateLimitInfo { Requests = new List<DateTime>() });

        lock (info)
        {
            // Remove old requests outside the window
            info.Requests.RemoveAll(t => now - t > _window);

            if (info.Requests.Count >= maxRequests)
            {
                return false;
            }

            info.Requests.Add(now);
            return true;
        }
    }

    private string GetClientIpAddress(HttpContext context)
    {
        // Check for forwarded IP (behind proxy/load balancer)
        var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedFor))
        {
            return forwardedFor.Split(',')[0].Trim();
        }

        return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }

    private class RateLimitInfo
    {
        public List<DateTime> Requests { get; set; } = new();
    }
}

