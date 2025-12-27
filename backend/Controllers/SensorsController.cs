using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCampus.API.Services;
using SmartCampus.API.DTOs;

namespace SmartCampus.API.Controllers;

/// <summary>
/// Controller for IoT sensor management and data endpoints
/// </summary>
[ApiController]
[Route("api/v1/sensors")]
[Authorize]
public class SensorsController : ControllerBase
{
    private readonly ISensorService _sensorService;
    private readonly ILogger<SensorsController> _logger;

    public SensorsController(
        ISensorService sensorService,
        ILogger<SensorsController> logger)
    {
        _sensorService = sensorService;
        _logger = logger;
    }

    /// <summary>
    /// Get all sensors with current readings
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<SensorListResponseDto>> GetSensors()
    {
        try
        {
            var sensors = await _sensorService.GetAllSensorsAsync();
            return Ok(new SensorListResponseDto
            {
                Sensors = sensors,
                TotalCount = sensors.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting sensors");
            return StatusCode(500, new { message = "Sensörler alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Get sensor by ID
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<SensorDto>> GetSensorById(Guid id)
    {
        try
        {
            var sensor = await _sensorService.GetSensorByIdAsync(id);
            if (sensor == null)
            {
                return NotFound(new { message = "Sensör bulunamadı." });
            }
            return Ok(sensor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting sensor by ID");
            return StatusCode(500, new { message = "Sensör alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Get sensor data with optional time range filter
    /// </summary>
    [HttpGet("{id}/data")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<SensorDataResponseDto>> GetSensorData(
        Guid id,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] int? limit = null)
    {
        try
        {
            var data = await _sensorService.GetSensorDataAsync(id, fromDate, toDate, limit);
            return Ok(data);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting sensor data");
            return StatusCode(500, new { message = "Sensör verisi alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Get aggregated sensor data (hourly or daily)
    /// </summary>
    [HttpGet("{id}/data/aggregate")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<SensorDataAggregationResponseDto>> GetSensorDataAggregation(
        Guid id,
        [FromQuery] string aggregation = "hour", // "hour" or "day"
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null)
    {
        try
        {
            if (aggregation != "hour" && aggregation != "day")
            {
                return BadRequest(new { message = "Aggregation type must be 'hour' or 'day'" });
            }

            var data = await _sensorService.GetSensorDataAggregationAsync(id, aggregation, fromDate, toDate);
            return Ok(data);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting aggregated sensor data");
            return StatusCode(500, new { message = "Toplu sensör verisi alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Get recent anomalies/alerts
    /// </summary>
    [HttpGet("anomalies")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<SensorAnomalyDto>>> GetRecentAnomalies(
        [FromQuery] int limit = 50)
    {
        try
        {
            var anomalies = await _sensorService.GetRecentAnomaliesAsync(limit);
            return Ok(anomalies);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting anomalies");
            return StatusCode(500, new { message = "Anomaliler alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Get anomalies for a specific sensor
    /// </summary>
    [HttpGet("{id}/anomalies")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<SensorAnomalyDto>>> GetSensorAnomalies(
        Guid id,
        [FromQuery] int limit = 50)
    {
        try
        {
            var anomalies = await _sensorService.GetSensorAnomaliesAsync(id, limit);
            return Ok(anomalies);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting sensor anomalies");
            return StatusCode(500, new { message = "Sensör anomalileri alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Generate mock data for a sensor (for testing/demo)
    /// </summary>
    [HttpPost("{id}/generate-mock-data")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> GenerateMockData(
        Guid id,
        [FromQuery] int count = 100)
    {
        try
        {
            await _sensorService.GenerateMockSensorDataAsync(id, count);
            return Ok(new { message = $"{count} adet mock veri oluşturuldu." });
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating mock data");
            return StatusCode(500, new { message = "Mock veri oluşturulurken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Generate mock data for all sensors (for testing/demo)
    /// </summary>
    [HttpPost("generate-mock-data-all")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> GenerateMockDataForAll()
    {
        try
        {
            await _sensorService.GenerateMockDataForAllSensorsAsync();
            return Ok(new { message = "Tüm sensörler için mock veri oluşturuldu." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating mock data for all sensors");
            return StatusCode(500, new { message = "Mock veri oluşturulurken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Generate all mock sensors and their initial data (for testing/demo)
    /// </summary>
    [HttpPost("generate-all-sensors-and-data")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> GenerateAllSensorsAndData()
    {
        try
        {
            await _sensorService.GenerateAllMockSensorsAndDataAsync();
            return Ok(new { message = "Tüm mock sensörler ve verileri oluşturuldu." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating all sensors and data");
            return StatusCode(500, new { message = "Sensörler ve verileri oluşturulurken bir hata oluştu." });
        }
    }
}

