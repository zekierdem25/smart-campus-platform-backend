using SmartCampus.API.DTOs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

/// <summary>
/// Service interface for IoT sensor management and data processing
/// </summary>
public interface ISensorService
{
    // Sensor CRUD
    Task<List<SensorDto>> GetAllSensorsAsync();
    Task<SensorDto?> GetSensorByIdAsync(Guid id);
    Task<SensorDto?> GetSensorBySensorIdAsync(string sensorId);
    
    // Sensor Data
    Task<SensorDataResponseDto> GetSensorDataAsync(
        Guid sensorId, 
        DateTime? fromDate = null, 
        DateTime? toDate = null,
        int? limit = null);
    
    Task<SensorDataAggregationResponseDto> GetSensorDataAggregationAsync(
        Guid sensorId,
        string aggregationType, // "hour" or "day"
        DateTime? fromDate = null,
        DateTime? toDate = null);
    
    // Anomaly Detection
    Task<List<SensorAnomalyDto>> GetRecentAnomaliesAsync(int limit = 50);
    Task<List<SensorAnomalyDto>> GetSensorAnomaliesAsync(Guid sensorId, int limit = 50);
    
    // Mock Data Generation (for demo/testing)
    Task GenerateMockSensorDataAsync(Guid sensorId, int count = 100);
    Task GenerateMockDataForAllSensorsAsync();
    Task GenerateAllMockSensorsAndDataAsync();
    
    // Real-time data generation (for WebSocket streaming)
    Task<SensorStreamDataDto> GenerateNextReadingAsync(Guid sensorId);
}

