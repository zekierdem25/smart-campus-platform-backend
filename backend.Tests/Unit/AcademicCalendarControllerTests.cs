using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Controllers;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class AcademicCalendarControllerTests
{
    private readonly ApplicationDbContext _context;

    public AcademicCalendarControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
    }

    [Fact]
    public void Constructor_WithValidContext_ShouldInitializeCorrectly()
    {
        var controller = new AcademicCalendarController(_context);
        Assert.NotNull(controller);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new AcademicCalendarController(null!));
    }

    [Fact]
    public async Task GetAcademicEvents_NoFilters_ShouldReturnAllEvents()
    {
        // Arrange
        var controller = new AcademicCalendarController(_context);
        var event1 = new AcademicEvent { Id = Guid.NewGuid(), Title = "Event 1", StartDate = DateTime.UtcNow.AddDays(1), EndDate = DateTime.UtcNow.AddDays(2), Type = AcademicEventType.General };
        var event2 = new AcademicEvent { Id = Guid.NewGuid(), Title = "Event 2", StartDate = DateTime.UtcNow.AddDays(5), EndDate = DateTime.UtcNow.AddDays(6), Type = AcademicEventType.Holiday };
        
        _context.AcademicEvents.AddRange(event1, event2);
        await _context.SaveChangesAsync();

        // Act
        var result = await controller.GetAcademicEvents();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedEvents = Assert.IsType<List<AcademicEventDto>>(actionResult.Value);
        Assert.Equal(2, returnedEvents.Count);
    }

    [Fact]
    public async Task GetAcademicEvents_DateFilter_ShouldReturnFilteredEvents()
    {
        // Arrange
        var controller = new AcademicCalendarController(_context);
        var futureEvent = new AcademicEvent { Id = Guid.NewGuid(), Title = "Future", StartDate = DateTime.UtcNow.AddDays(10), EndDate = DateTime.UtcNow.AddDays(11), Type = AcademicEventType.General };
        var pastEvent = new AcademicEvent { Id = Guid.NewGuid(), Title = "Past", StartDate = DateTime.UtcNow.AddDays(-10), EndDate = DateTime.UtcNow.AddDays(-9), Type = AcademicEventType.General };
        
        _context.AcademicEvents.AddRange(futureEvent, pastEvent);
        await _context.SaveChangesAsync();

        // Act (Filter valid for future event)
        var result = await controller.GetAcademicEvents(startDate: DateTime.UtcNow);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedEvents = Assert.IsType<List<AcademicEventDto>>(actionResult.Value);
        Assert.Single(returnedEvents);
        Assert.Equal("Future", returnedEvents[0].Title);
    }

    [Fact]
    public async Task GetAcademicEvent_ExistingId_ShouldReturnEvent()
    {
        // Arrange
        var controller = new AcademicCalendarController(_context);
        var evt = new AcademicEvent { Id = Guid.NewGuid(), Title = "Test Event", StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddHours(2), Type = AcademicEventType.Exam };
        _context.AcademicEvents.Add(evt);
        await _context.SaveChangesAsync();

        // Act
        var result = await controller.GetAcademicEvent(evt.Id);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedEvent = Assert.IsType<AcademicEventDto>(actionResult.Value);
        Assert.Equal(evt.Id, returnedEvent.Id);
    }

    [Fact]
    public async Task GetAcademicEvent_NonExistingId_ShouldReturnNotFound()
    {
        // Arrange
        var controller = new AcademicCalendarController(_context);

        // Act
        var result = await controller.GetAcademicEvent(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateAcademicEvent_ValidData_ShouldCreateEvent()
    {
        // Arrange
        var controller = new AcademicCalendarController(_context);
        var request = new CreateAcademicEventDto
        {
            Title = "New Event",
            StartDate = DateTime.UtcNow.AddDays(1),
            EndDate = DateTime.UtcNow.AddDays(2),
            Type = "Holiday",
            Description = "Test Description"
        };

        // Act
        var result = await controller.CreateAcademicEvent(request);

        // Assert
        var actionResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status201Created, actionResult.StatusCode);
        var createdEvent = Assert.IsType<AcademicEventDto>(actionResult.Value);
        Assert.Equal(request.Title, createdEvent.Title);
        Assert.Equal(AcademicEventType.Holiday.ToString(), createdEvent.Type);
    }

    [Fact]
    public async Task CreateAcademicEvent_InvalidDate_ShouldReturnBadRequest()
    {
        // Arrange
        var controller = new AcademicCalendarController(_context);
        var request = new CreateAcademicEventDto
        {
            Title = "Bad Event",
            StartDate = DateTime.UtcNow.AddDays(5),
            EndDate = DateTime.UtcNow.AddDays(1), // End before Start
            Type = "General"
        };

        // Act
        var result = await controller.CreateAcademicEvent(request);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}
