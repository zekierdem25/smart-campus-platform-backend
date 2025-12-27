namespace SmartCampus.API.DTOs;

/// <summary>
/// DTO for sensor information
/// </summary>
public class SensorDto
{
    public Guid Id { get; set; }
    public string SensorId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal? MinThreshold { get; set; }
    public decimal? MaxThreshold { get; set; }
    public bool IsActive { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Current reading (latest value)
    public decimal? CurrentValue { get; set; }
    public DateTime? LastReadingTime { get; set; }
}

/// <summary>
/// DTO for IoT sensor data reading
/// </summary>
public class IoTSensorDataDto
{
    public Guid Id { get; set; }
    public Guid SensorId { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public bool IsAnomaly { get; set; }
    public string? AnomalyReason { get; set; }
}

/// <summary>
/// DTO for sensor data with aggregation
/// </summary>
public class SensorDataAggregationDto
{
    public DateTime Timestamp { get; set; }
    public decimal? Average { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
    public int Count { get; set; }
}

/// <summary>
/// DTO for sensor list response
/// </summary>
public class SensorListResponseDto
{
    public List<SensorDto> Sensors { get; set; } = new();
    public int TotalCount { get; set; }
}

/// <summary>
/// DTO for sensor data response
/// </summary>
public class SensorDataResponseDto
{
    public Guid SensorId { get; set; }
    public string SensorName { get; set; } = string.Empty;
    public List<IoTSensorDataDto> Data { get; set; } = new();
    public int TotalCount { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

/// <summary>
/// DTO for aggregated sensor data response
/// </summary>
public class SensorDataAggregationResponseDto
{
    public Guid SensorId { get; set; }
    public string SensorName { get; set; } = string.Empty;
    public string AggregationType { get; set; } = string.Empty; // "hour", "day"
    public List<SensorDataAggregationDto> Data { get; set; } = new();
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

/// <summary>
/// DTO for real-time sensor stream data
/// </summary>
public class SensorStreamDataDto
{
    public Guid SensorId { get; set; }
    public string SensorName { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public bool IsAnomaly { get; set; }
    public string? AnomalyReason { get; set; }
}

/// <summary>
/// DTO for anomaly/alert information
/// </summary>
public class SensorAnomalyDto
{
    public Guid Id { get; set; }
    public Guid SensorId { get; set; }
    public string SensorName { get; set; } = string.Empty;
    public string SensorType { get; set; } = string.Empty;
    public string? Location { get; set; }
    public decimal Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string AnomalyReason { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string Severity { get; set; } = string.Empty; // "warning", "critical"
}

