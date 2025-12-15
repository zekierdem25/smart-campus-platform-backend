using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

public interface IAttendanceService
{
    double CalculateDistance(decimal lat1, decimal lon1, decimal lat2, decimal lon2);
    bool IsWithinGeofence(decimal studentLat, decimal studentLon, decimal classroomLat, decimal classroomLon, int radiusMeters, decimal accuracy);
    Task<AttendanceRecord?> GetLastAttendanceRecordAsync(Guid studentId);
    string GenerateQrCode(Guid sessionId);
    bool ValidateQrCode(string qrCode, Guid sessionId);
}

public class AttendanceService : IAttendanceService
{
    private readonly ApplicationDbContext _context;
    private readonly IJwtService _jwtService;
    private const double EarthRadiusMeters = 6371e3; // Earth radius in meters

    public AttendanceService(ApplicationDbContext context, IJwtService jwtService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
    }

    /// <summary>
    /// Calculates distance between two GPS coordinates using Haversine formula.
    /// Returns distance in meters.
    /// 
    /// Formula: distance = 2 * R * asin(sqrt(sin²(Δlat/2) + cos(lat1) * cos(lat2) * sin²(Δlon/2)))
    /// </summary>
    public double CalculateDistance(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
    {
        // Convert to radians
        double φ1 = (double)lat1 * Math.PI / 180;
        double φ2 = (double)lat2 * Math.PI / 180;
        double Δφ = ((double)lat2 - (double)lat1) * Math.PI / 180;
        double Δλ = ((double)lon2 - (double)lon1) * Math.PI / 180;

        // Haversine formula
        double a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                   Math.Cos(φ1) * Math.Cos(φ2) *
                   Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
        
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double distance = EarthRadiusMeters * c;
        
        return distance;
    }

    /// <summary>
    /// Checks if student's location is within the geofence radius (+ GPS accuracy buffer).
    /// </summary>
    public bool IsWithinGeofence(decimal studentLat, decimal studentLon, decimal classroomLat, decimal classroomLon, int radiusMeters, decimal accuracy)
    {
        double distance = CalculateDistance(studentLat, studentLon, classroomLat, classroomLon);
        
        // Allow for GPS accuracy buffer (5 meters extra)
        double allowedRadius = radiusMeters + (double)accuracy + 5;
        
        return distance <= allowedRadius;
    }

    /// <summary>
    /// Gets the last attendance record for a student (for velocity check).
    /// </summary>
    public async Task<AttendanceRecord?> GetLastAttendanceRecordAsync(Guid studentId)
    {
        return await _context.AttendanceRecords
            .Where(ar => ar.StudentId == studentId)
            .OrderByDescending(ar => ar.CheckInTime)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Generates a QR code token for attendance session.
    /// Token expires in 60 seconds for security while allowing time for scanning.
    /// </summary>
    public string GenerateQrCode(Guid sessionId)
    {
        // Simple JWT-like token with session ID and expiry
        var payload = new
        {
            sessionId = sessionId.ToString(),
            exp = DateTimeOffset.UtcNow.AddSeconds(60).ToUnixTimeSeconds(), // 60 seconds expiry
            nonce = Guid.NewGuid().ToString("N")[..8]
        };

        // For simplicity, use base64 encoding. In production, use proper JWT.
        var json = System.Text.Json.JsonSerializer.Serialize(payload);
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json));
    }

    /// <summary>
    /// Validates QR code token.
    /// </summary>
    public bool ValidateQrCode(string qrCode, Guid sessionId)
    {
        try
        {
            var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(qrCode));
            var payload = System.Text.Json.JsonSerializer.Deserialize<QrPayload>(json, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (payload == null)
                return false;

            // Check session ID
            if (!Guid.TryParse(payload.SessionId, out var qrSessionId) || qrSessionId != sessionId)
                return false;

            // Check expiry
            var expiry = DateTimeOffset.FromUnixTimeSeconds(payload.Exp);
            if (DateTimeOffset.UtcNow > expiry)
                return false;

            return true;
        }
        catch
        {
            return false;
        }
    }

    private class QrPayload
    {
        public string SessionId { get; set; } = string.Empty;
        public long Exp { get; set; }
        public string Nonce { get; set; } = string.Empty;
    }
}
