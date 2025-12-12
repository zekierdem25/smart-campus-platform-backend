using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

public interface ISpoofingDetectionService
{
    Task<SpoofingCheckResult> CheckForSpoofingAsync(Guid studentId, decimal latitude, decimal longitude, string? ipAddress, DateTime checkInTime, bool? isMockLocation);
    bool IsCampusIp(string? ipAddress);
}

public class SpoofingDetectionService : ISpoofingDetectionService
{
    private readonly ApplicationDbContext _context;
    private readonly IAttendanceService _attendanceService;
    private readonly IConfiguration _configuration;
    private readonly List<(IPAddress network, int prefixLength)> _campusNetworks;

    // Maximum reasonable velocity in m/s (360 km/h = 100 m/s - faster than any human can travel)
    private const double MaxVelocityMps = 100;
    
    // Minimum time between check-ins to trigger velocity check (seconds)
    private const int MinTimeDifferenceSeconds = 60;

    public SpoofingDetectionService(
        ApplicationDbContext context, 
        IAttendanceService attendanceService,
        IConfiguration configuration)
    {
        _context = context;
        _attendanceService = attendanceService;
        _configuration = configuration;
        _campusNetworks = ParseCampusNetworks();
    }

    /// <summary>
    /// Comprehensive spoofing detection check.
    /// Checks:
    /// 1. Mock location flag from device
    /// 2. Campus IP validation (if enabled)
    /// 3. Velocity check (impossible travel)
    /// </summary>
    public async Task<SpoofingCheckResult> CheckForSpoofingAsync(
        Guid studentId, 
        decimal latitude, 
        decimal longitude, 
        string? ipAddress, 
        DateTime checkInTime,
        bool? isMockLocation)
    {
        // Check 1: Mock location flag
        if (isMockLocation == true)
        {
            return new SpoofingCheckResult 
            { 
                IsSuspicious = true, 
                Reason = "MOCK_LOCATION_DETECTED" 
            };
        }

        // Check 2: Campus IP validation (if not bypassed)
        var bypassIpCheck = _configuration.GetValue<bool>("CampusSettings:BypassIpCheck", true);
        if (!bypassIpCheck && !IsCampusIp(ipAddress))
        {
            return new SpoofingCheckResult 
            { 
                IsSuspicious = true, 
                Reason = "IP_NOT_CAMPUS" 
            };
        }

        // Check 3: Velocity check (impossible travel)
        var lastRecord = await _attendanceService.GetLastAttendanceRecordAsync(studentId);
        if (lastRecord != null)
        {
            var timeDiff = (checkInTime - lastRecord.CheckInTime).TotalSeconds;
            
            // Only check if enough time has passed
            if (timeDiff > MinTimeDifferenceSeconds)
            {
                var distance = _attendanceService.CalculateDistance(
                    lastRecord.Latitude, 
                    lastRecord.Longitude, 
                    latitude, 
                    longitude);

                var velocity = distance / timeDiff; // m/s

                if (velocity > MaxVelocityMps)
                {
                    return new SpoofingCheckResult 
                    { 
                        IsSuspicious = true, 
                        Reason = "VELOCITY_IMPOSSIBLE",
                        VelocityMps = (decimal)velocity
                    };
                }
            }
        }

        // All checks passed
        return new SpoofingCheckResult { IsSuspicious = false };
    }

    /// <summary>
    /// Validates if the given IP address is within campus network ranges.
    /// </summary>
    public bool IsCampusIp(string? ipAddress)
    {
        if (string.IsNullOrEmpty(ipAddress))
            return false;

        // Local/loopback addresses are always allowed (for development)
        if (ipAddress == "127.0.0.1" || ipAddress == "::1" || ipAddress.StartsWith("192.168.") || ipAddress.StartsWith("10."))
            return true;

        if (!_campusNetworks.Any())
            return true; // If no campus networks configured, allow all

        if (!IPAddress.TryParse(ipAddress, out var clientIp))
            return false;

        foreach (var (network, prefixLength) in _campusNetworks)
        {
            if (IsInNetwork(clientIp, network, prefixLength))
                return true;
        }

        return false;
    }

    private List<(IPAddress network, int prefixLength)> ParseCampusNetworks()
    {
        var networks = new List<(IPAddress, int)>();
        var configNetworks = _configuration.GetSection("CampusSettings:AllowedIpRanges").Get<List<string>>();

        if (configNetworks == null)
            return networks;

        foreach (var cidr in configNetworks)
        {
            var parts = cidr.Split('/');
            if (parts.Length == 2 && 
                IPAddress.TryParse(parts[0], out var network) && 
                int.TryParse(parts[1], out var prefix))
            {
                networks.Add((network, prefix));
            }
        }

        return networks;
    }

    private bool IsInNetwork(IPAddress address, IPAddress network, int prefixLength)
    {
        var addressBytes = address.GetAddressBytes();
        var networkBytes = network.GetAddressBytes();

        if (addressBytes.Length != networkBytes.Length)
            return false;

        int byteCount = prefixLength / 8;
        int bitCount = prefixLength % 8;

        for (int i = 0; i < byteCount; i++)
        {
            if (addressBytes[i] != networkBytes[i])
                return false;
        }

        if (bitCount > 0 && byteCount < addressBytes.Length)
        {
            int mask = 0xFF << (8 - bitCount);
            if ((addressBytes[byteCount] & mask) != (networkBytes[byteCount] & mask))
                return false;
        }

        return true;
    }
}
