using System.Text.Json;
using SmartCampus.API.DTOs;

namespace SmartCampus.API.Services;

public interface IScheduleConflictService
{
    bool HasScheduleConflict(List<ScheduleSlotDto> existingSchedule, List<ScheduleSlotDto> newSchedule);
    List<ScheduleSlotDto> ParseScheduleJson(string? scheduleJson);
    string SerializeSchedule(List<ScheduleSlotDto> schedule);
}

public class ScheduleConflictService : IScheduleConflictService
{
    /// <summary>
    /// Checks if there's any time conflict between existing schedule and new schedule.
    /// </summary>
    public bool HasScheduleConflict(List<ScheduleSlotDto> existingSchedule, List<ScheduleSlotDto> newSchedule)
    {
        foreach (var existing in existingSchedule)
        {
            foreach (var newSlot in newSchedule)
            {
                if (string.Equals(existing.Day, newSlot.Day, StringComparison.OrdinalIgnoreCase))
                {
                    if (TimeOverlap(existing, newSlot))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Checks if two time slots overlap.
    /// Overlap occurs when: startA < endB AND startB < endA
    /// </summary>
    private bool TimeOverlap(ScheduleSlotDto a, ScheduleSlotDto b)
    {
        var startA = TimeSpan.Parse(a.StartTime);
        var endA = TimeSpan.Parse(a.EndTime);
        var startB = TimeSpan.Parse(b.StartTime);
        var endB = TimeSpan.Parse(b.EndTime);

        return startA < endB && startB < endA;
    }

    /// <summary>
    /// Parses schedule JSON string to list of ScheduleSlotDto.
    /// </summary>
    public List<ScheduleSlotDto> ParseScheduleJson(string? scheduleJson)
    {
        if (string.IsNullOrEmpty(scheduleJson))
            return new List<ScheduleSlotDto>();

        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<ScheduleSlotDto>>(scheduleJson, options) ?? new List<ScheduleSlotDto>();
        }
        catch
        {
            return new List<ScheduleSlotDto>();
        }
    }

    /// <summary>
    /// Serializes schedule list to JSON string.
    /// </summary>
    public string SerializeSchedule(List<ScheduleSlotDto> schedule)
    {
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        return JsonSerializer.Serialize(schedule, options);
    }
}
