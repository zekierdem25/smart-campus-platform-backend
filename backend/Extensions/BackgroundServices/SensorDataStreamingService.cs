using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartCampus.API.Data;
using SmartCampus.API.Hubs;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using SmartCampus.API.DTOs;

namespace SmartCampus.API.Extensions.BackgroundServices;

/// <summary>
/// Background service that periodically generates and streams sensor data via SignalR
/// </summary>
public class SensorDataStreamingService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SensorDataStreamingService> _logger;
    private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(5); // Update every 5 seconds

    public SensorDataStreamingService(
        IServiceProvider serviceProvider,
        ILogger<SensorDataStreamingService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("SensorDataStreamingService başlatıldı. Her {Interval} saniyede bir sensor data gönderilecek.", _updateInterval.TotalSeconds);

        // Wait a bit before starting to ensure the app is fully initialized
        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var sensorService = scope.ServiceProvider.GetRequiredService<ISensorService>();
                var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<SensorHub>>();

                // Get all active sensors
                var activeSensors = await context.Sensors
                    .Where(s => s.IsActive && s.Status != SensorStatus.Offline)
                    .ToListAsync(stoppingToken);

                if (activeSensors.Count == 0)
                {
                    _logger.LogWarning("Aktif sensör bulunamadı. 30 saniye sonra tekrar denenecek.");
                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                    continue;
                }

                // Generate and send data for each sensor
                foreach (var sensor in activeSensors)
                {
                    try
                    {
                        // Generate next reading
                        var streamData = await sensorService.GenerateNextReadingAsync(sensor.Id);

                        // Broadcast to all users in "all_sensors" group
                        await hubContext.Clients.Group("all_sensors").SendAsync(
                            "SensorDataUpdate",
                            new
                            {
                                sensorId = streamData.SensorId,
                                sensorName = streamData.SensorName,
                                value = streamData.Value,
                                unit = streamData.Unit,
                                timestamp = streamData.Timestamp,
                                isAnomaly = streamData.IsAnomaly,
                                anomalyReason = streamData.AnomalyReason,
                                severity = streamData.IsAnomaly ? (streamData.Value > sensor.MaxThreshold || streamData.Value < sensor.MinThreshold ? "critical" : "warning") : "normal",
                                type = sensor.Type.ToString(),
                                location = sensor.Location
                            },
                            cancellationToken: stoppingToken);

                        // Also send to specific sensor group
                        await hubContext.Clients.Group($"sensor_{sensor.Id}").SendAsync(
                            "SensorDataUpdate",
                            new
                            {
                                sensorId = streamData.SensorId,
                                sensorName = streamData.SensorName,
                                value = streamData.Value,
                                unit = streamData.Unit,
                                timestamp = streamData.Timestamp,
                                isAnomaly = streamData.IsAnomaly,
                                anomalyReason = streamData.AnomalyReason,
                                severity = streamData.IsAnomaly ? (streamData.Value > sensor.MaxThreshold || streamData.Value < sensor.MinThreshold ? "critical" : "warning") : "normal",
                                type = sensor.Type.ToString(),
                                location = sensor.Location
                            },
                            cancellationToken: stoppingToken);

                        _logger.LogDebug("Sensor data gönderildi: {SensorId} = {Value} {Unit}", 
                            sensor.SensorId, streamData.Value, streamData.Unit);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Sensor {SensorId} için data gönderilirken hata oluştu", sensor.SensorId);
                    }
                }

                // Wait before next update
                await Task.Delay(_updateInterval, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("SensorDataStreamingService durduruldu.");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SensorDataStreamingService çalışırken hata oluştu");
                // Wait a bit longer on error before retrying
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("SensorDataStreamingService durduruluyor...");
        await base.StopAsync(cancellationToken);
    }
}

