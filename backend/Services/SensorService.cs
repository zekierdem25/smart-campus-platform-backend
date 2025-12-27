using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

public class SensorService : ISensorService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SensorService> _logger;
    private readonly Random _random = new();

    public SensorService(
        ApplicationDbContext context,
        ILogger<SensorService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<SensorDto>> GetAllSensorsAsync()
    {
        var sensors = await _context.Sensors
            .Where(s => s.IsActive)
            .OrderBy(s => s.Name)
            .ToListAsync();

        var sensorDtos = new List<SensorDto>();

        foreach (var sensor in sensors)
        {
            // Get latest reading
            var latestData = await _context.SensorData
                .Where(sd => sd.SensorId == sensor.Id)
                .OrderByDescending(sd => sd.Timestamp)
                .FirstOrDefaultAsync();

            sensorDtos.Add(new SensorDto
            {
                Id = sensor.Id,
                SensorId = sensor.SensorId,
                Name = sensor.Name,
                Type = sensor.Type.ToString(),
                Location = sensor.Location,
                Status = sensor.Status.ToString(),
                Unit = sensor.Unit,
                MinThreshold = sensor.MinThreshold,
                MaxThreshold = sensor.MaxThreshold,
                IsActive = sensor.IsActive,
                Description = sensor.Description,
                CreatedAt = sensor.CreatedAt,
                UpdatedAt = sensor.UpdatedAt,
                CurrentValue = latestData?.Value,
                LastReadingTime = latestData?.Timestamp
            });
        }

        return sensorDtos;
    }

    public async Task<SensorDto?> GetSensorByIdAsync(Guid id)
    {
        var sensor = await _context.Sensors
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sensor == null) return null;

        var latestData = await _context.SensorData
            .Where(sd => sd.SensorId == sensor.Id)
            .OrderByDescending(sd => sd.Timestamp)
            .FirstOrDefaultAsync();

        return new SensorDto
        {
            Id = sensor.Id,
            SensorId = sensor.SensorId,
            Name = sensor.Name,
            Type = sensor.Type.ToString(),
            Location = sensor.Location,
            Status = sensor.Status.ToString(),
            Unit = sensor.Unit,
            MinThreshold = sensor.MinThreshold,
            MaxThreshold = sensor.MaxThreshold,
            IsActive = sensor.IsActive,
            Description = sensor.Description,
            CreatedAt = sensor.CreatedAt,
            UpdatedAt = sensor.UpdatedAt,
            CurrentValue = latestData?.Value,
            LastReadingTime = latestData?.Timestamp
        };
    }

    public async Task<SensorDto?> GetSensorBySensorIdAsync(string sensorId)
    {
        var sensor = await _context.Sensors
            .FirstOrDefaultAsync(s => s.SensorId == sensorId);

        if (sensor == null) return null;

        var latestData = await _context.SensorData
            .Where(sd => sd.SensorId == sensor.Id)
            .OrderByDescending(sd => sd.Timestamp)
            .FirstOrDefaultAsync();

        return new SensorDto
        {
            Id = sensor.Id,
            SensorId = sensor.SensorId,
            Name = sensor.Name,
            Type = sensor.Type.ToString(),
            Location = sensor.Location,
            Status = sensor.Status.ToString(),
            Unit = sensor.Unit,
            MinThreshold = sensor.MinThreshold,
            MaxThreshold = sensor.MaxThreshold,
            IsActive = sensor.IsActive,
            Description = sensor.Description,
            CreatedAt = sensor.CreatedAt,
            UpdatedAt = sensor.UpdatedAt,
            CurrentValue = latestData?.Value,
            LastReadingTime = latestData?.Timestamp
        };
    }

    public async Task<SensorDataResponseDto> GetSensorDataAsync(
        Guid sensorId,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        int? limit = null)
    {
        var sensor = await _context.Sensors.FindAsync(sensorId);
        if (sensor == null)
        {
            throw new ArgumentException($"Sensor with ID {sensorId} not found");
        }

        var query = _context.SensorData
            .Where(sd => sd.SensorId == sensorId)
            .AsQueryable();

        if (fromDate.HasValue)
        {
            query = query.Where(sd => sd.Timestamp >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(sd => sd.Timestamp <= toDate.Value);
        }

        query = query.OrderByDescending(sd => sd.Timestamp);

        if (limit.HasValue)
        {
            query = query.Take(limit.Value);
        }

        var data = await query.ToListAsync();
        var totalCount = await _context.SensorData
            .Where(sd => sd.SensorId == sensorId &&
                        (!fromDate.HasValue || sd.Timestamp >= fromDate.Value) &&
                        (!toDate.HasValue || sd.Timestamp <= toDate.Value))
            .CountAsync();

        return new SensorDataResponseDto
        {
            SensorId = sensorId,
            SensorName = sensor.Name,
            Data = data.Select(sd => new IoTSensorDataDto
            {
                Id = sd.Id,
                SensorId = sd.SensorId,
                Timestamp = sd.Timestamp,
                Value = sd.Value,
                Unit = sd.Unit,
                IsAnomaly = sd.IsAnomaly,
                AnomalyReason = sd.AnomalyReason
            }).ToList(),
            TotalCount = totalCount,
            FromDate = fromDate,
            ToDate = toDate
        };
    }

    public async Task<SensorDataAggregationResponseDto> GetSensorDataAggregationAsync(
        Guid sensorId,
        string aggregationType,
        DateTime? fromDate = null,
        DateTime? toDate = null)
    {
        var sensor = await _context.Sensors.FindAsync(sensorId);
        if (sensor == null)
        {
            throw new ArgumentException($"Sensor with ID {sensorId} not found");
        }

        var query = _context.SensorData
            .Where(sd => sd.SensorId == sensorId)
            .AsQueryable();

        if (fromDate.HasValue)
        {
            query = query.Where(sd => sd.Timestamp >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(sd => sd.Timestamp <= toDate.Value);
        }

        List<SensorDataAggregationDto> aggregatedData;

        if (aggregationType.ToLower() == "hour")
        {
            aggregatedData = await query
                .GroupBy(sd => new
                {
                    sd.Timestamp.Year,
                    sd.Timestamp.Month,
                    sd.Timestamp.Day,
                    sd.Timestamp.Hour
                })
                .Select(g => new SensorDataAggregationDto
                {
                    Timestamp = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day, g.Key.Hour, 0, 0),
                    Average = g.Average(sd => sd.Value),
                    Min = g.Min(sd => sd.Value),
                    Max = g.Max(sd => sd.Value),
                    Count = g.Count()
                })
                .OrderBy(d => d.Timestamp)
                .ToListAsync();
        }
        else if (aggregationType.ToLower() == "day")
        {
            aggregatedData = await query
                .GroupBy(sd => new
                {
                    sd.Timestamp.Year,
                    sd.Timestamp.Month,
                    sd.Timestamp.Day
                })
                .Select(g => new SensorDataAggregationDto
                {
                    Timestamp = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day),
                    Average = g.Average(sd => sd.Value),
                    Min = g.Min(sd => sd.Value),
                    Max = g.Max(sd => sd.Value),
                    Count = g.Count()
                })
                .OrderBy(d => d.Timestamp)
                .ToListAsync();
        }
        else
        {
            throw new ArgumentException($"Invalid aggregation type: {aggregationType}. Use 'hour' or 'day'");
        }

        return new SensorDataAggregationResponseDto
        {
            SensorId = sensorId,
            SensorName = sensor.Name,
            AggregationType = aggregationType,
            Data = aggregatedData,
            FromDate = fromDate,
            ToDate = toDate
        };
    }

    public async Task<List<SensorAnomalyDto>> GetRecentAnomaliesAsync(int limit = 50)
    {
        var anomalies = await _context.SensorData
            .Where(sd => sd.IsAnomaly)
            .Include(sd => sd.Sensor)
            .OrderByDescending(sd => sd.Timestamp)
            .Take(limit)
            .ToListAsync();

        return anomalies.Select(a => new SensorAnomalyDto
        {
            Id = a.Id,
            SensorId = a.SensorId,
            SensorName = a.Sensor.Name,
            SensorType = a.Sensor.Type.ToString(),
            Location = a.Sensor.Location,
            Value = a.Value,
            Unit = a.Unit,
            AnomalyReason = a.AnomalyReason ?? "Unknown",
            Timestamp = a.Timestamp,
            Severity = DetermineSeverity(a.Sensor, a.Value)
        }).ToList();
    }

    public async Task<List<SensorAnomalyDto>> GetSensorAnomaliesAsync(Guid sensorId, int limit = 50)
    {
        var anomalies = await _context.SensorData
            .Where(sd => sd.SensorId == sensorId && sd.IsAnomaly)
            .Include(sd => sd.Sensor)
            .OrderByDescending(sd => sd.Timestamp)
            .Take(limit)
            .ToListAsync();

        return anomalies.Select(a => new SensorAnomalyDto
        {
            Id = a.Id,
            SensorId = a.SensorId,
            SensorName = a.Sensor.Name,
            SensorType = a.Sensor.Type.ToString(),
            Location = a.Sensor.Location,
            Value = a.Value,
            Unit = a.Unit,
            AnomalyReason = a.AnomalyReason ?? "Unknown",
            Timestamp = a.Timestamp,
            Severity = DetermineSeverity(a.Sensor, a.Value)
        }).ToList();
    }

    public async Task GenerateMockSensorDataAsync(Guid sensorId, int count = 100)
    {
        var sensor = await _context.Sensors.FindAsync(sensorId);
        if (sensor == null)
        {
            throw new ArgumentException($"Sensor with ID {sensorId} not found");
        }

        var now = DateTime.UtcNow;
        var sensorDataList = new List<SensorData>();

        for (int i = 0; i < count; i++)
        {
            var timestamp = now.AddMinutes(-(count - i) * 5); // 5 dakika aralıklarla
            var value = GenerateMockValue(sensor, timestamp);
            var isAnomaly = CheckAnomaly(sensor, value);
            var anomalyReason = isAnomaly ? DetermineAnomalyReason(sensor, value) : null;

            sensorDataList.Add(new SensorData
            {
                Id = Guid.NewGuid(),
                SensorId = sensorId,
                Timestamp = timestamp,
                Value = value,
                Unit = sensor.Unit,
                IsAnomaly = isAnomaly,
                AnomalyReason = anomalyReason,
                CreatedAt = DateTime.UtcNow
            });
        }

        await _context.SensorData.AddRangeAsync(sensorDataList);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Generated {count} mock data points for sensor {sensor.SensorId}");
    }

    public async Task GenerateMockDataForAllSensorsAsync()
    {
        var sensors = await _context.Sensors
            .Where(s => s.IsActive)
            .ToListAsync();

        foreach (var sensor in sensors)
        {
            try
            {
                await GenerateMockSensorDataAsync(sensor.Id, 100);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error generating mock data for sensor {sensor.SensorId}");
            }
        }
    }

    public async Task GenerateAllMockSensorsAndDataAsync()
    {
        _logger.LogInformation("Generating mock sensors and initial data...");

        var sensorsToGenerate = new List<(string name, SensorType type, string unit, decimal min, decimal max, string sensorId, string location)>
        {
            ("Ana Kampüs Giriş", SensorType.Occupancy, "people", 0, 200, "OCC-001", "Ana Giriş"),
            ("Kütüphane 1. Kat", SensorType.Temperature, "°C", 18, 25, "TEMP-001", "Kütüphane Binası"),
            ("Laboratuvar B Blok", SensorType.Humidity, "%", 30, 60, "HUM-001", "Laboratuvar Binası"),
            ("Yemekhane", SensorType.Energy, "kW", 50, 500, "ENG-001", "Yemekhane Binası"),
            ("Otopark Sensörü A", SensorType.Occupancy, "slots", 0, 150, "PARK-001", "Otopark Alanı"),
            ("Sunucu Odası", SensorType.Temperature, "°C", 15, 20, "TEMP-002", "IT Binası"),
            ("Amfi 101", SensorType.Occupancy, "people", 0, 100, "OCC-002", "Eğitim Binası"),
            ("Spor Salonu", SensorType.Energy, "kW", 100, 800, "ENG-002", "Spor Kompleksi"),
            ("Kantin", SensorType.Temperature, "°C", 20, 28, "TEMP-003", "Kantin Binası"),
            ("Giriş Kapısı", SensorType.AirQuality, "AQI", 0, 100, "AQI-001", "Ana Giriş")
        };

        foreach (var s in sensorsToGenerate)
        {
            var existingSensor = await _context.Sensors.FirstOrDefaultAsync(x => x.SensorId == s.sensorId);
            if (existingSensor == null)
            {
                var newSensor = new Sensor
                {
                    SensorId = s.sensorId,
                    Name = s.name,
                    Type = s.type,
                    Unit = s.unit,
                    MinThreshold = s.min,
                    MaxThreshold = s.max,
                    Location = s.location,
                    Description = $"{s.name} için {s.type} sensörü",
                    IsActive = true,
                    Status = SensorStatus.Active,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.Sensors.Add(newSensor);
                await _context.SaveChangesAsync(); // Save to get the Guid Id

                // Generate initial data for the new sensor
                await GenerateMockSensorDataAsync(newSensor.Id, 50); // Generate 50 data points
            }
        }
        _logger.LogInformation("Mock sensors and initial data generation complete.");
    }

    public async Task<SensorStreamDataDto> GenerateNextReadingAsync(Guid sensorId)
    {
        var sensor = await _context.Sensors.FindAsync(sensorId);
        if (sensor == null)
        {
            throw new ArgumentException($"Sensor with ID {sensorId} not found");
        }

        var now = DateTime.UtcNow;
        var value = GenerateMockValue(sensor, now);
        var isAnomaly = CheckAnomaly(sensor, value);
        var anomalyReason = isAnomaly ? DetermineAnomalyReason(sensor, value) : null;

        // Save to database
        var sensorData = new SensorData
        {
            Id = Guid.NewGuid(),
            SensorId = sensorId,
            Timestamp = now,
            Value = value,
            Unit = sensor.Unit,
            IsAnomaly = isAnomaly,
            AnomalyReason = anomalyReason,
            CreatedAt = DateTime.UtcNow
        };

        await _context.SensorData.AddAsync(sensorData);
        await _context.SaveChangesAsync();

        // Update sensor status if anomaly detected
        if (isAnomaly)
        {
            sensor.Status = DetermineSeverity(sensor, value) == "critical" 
                ? SensorStatus.Critical 
                : SensorStatus.Warning;
            sensor.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        return new SensorStreamDataDto
        {
            SensorId = sensorId,
            SensorName = sensor.Name,
            Value = value,
            Unit = sensor.Unit,
            Timestamp = now,
            IsAnomaly = isAnomaly,
            AnomalyReason = anomalyReason
        };
    }

    // Helper methods

    private decimal GenerateMockValue(Sensor sensor, DateTime timestamp)
    {
        // Generate realistic values based on sensor type
        return sensor.Type switch
        {
            SensorType.Temperature => 18 + (decimal)(_random.NextDouble() * 10), // 18-28°C
            SensorType.Humidity => 30 + (decimal)(_random.NextDouble() * 40), // 30-70%
            SensorType.Occupancy => _random.Next(0, 200), // 0-200 people
            SensorType.Energy => 50 + (decimal)(_random.NextDouble() * 500), // 50-550 kW
            SensorType.AirQuality => 20 + (decimal)(_random.NextDouble() * 60), // 20-80 AQI
            SensorType.Light => (decimal)(_random.NextDouble() * 1000), // 0-1000 lux
            SensorType.Motion => _random.Next(0, 2), // 0 or 1
            _ => (decimal)(_random.NextDouble() * 100)
        };
    }

    private bool CheckAnomaly(Sensor sensor, decimal value)
    {
        if (sensor.MinThreshold.HasValue && value < sensor.MinThreshold.Value)
            return true;
        
        if (sensor.MaxThreshold.HasValue && value > sensor.MaxThreshold.Value)
            return true;

        return false;
    }

    private string DetermineAnomalyReason(Sensor sensor, decimal value)
    {
        if (sensor.MinThreshold.HasValue && value < sensor.MinThreshold.Value)
            return $"Value below minimum threshold ({sensor.MinThreshold.Value} {sensor.Unit})";
        
        if (sensor.MaxThreshold.HasValue && value > sensor.MaxThreshold.Value)
            return $"Value above maximum threshold ({sensor.MaxThreshold.Value} {sensor.Unit})";

        return "Anomaly detected";
    }

    private string DetermineSeverity(Sensor sensor, decimal value)
    {
        if (!sensor.MinThreshold.HasValue || !sensor.MaxThreshold.HasValue)
            return "warning";

        var range = sensor.MaxThreshold.Value - sensor.MinThreshold.Value;
        var deviation = value < sensor.MinThreshold.Value
            ? sensor.MinThreshold.Value - value
            : value - sensor.MaxThreshold.Value;

        // If deviation is more than 50% of the range, it's critical
        if (deviation > range * 0.5m)
            return "critical";

        return "warning";
    }
}

