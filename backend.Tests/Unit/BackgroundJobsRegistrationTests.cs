using Hangfire;
using Hangfire.InMemory;
using SmartCampus.API.Services;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

[Collection("HangfireTests")]
public class BackgroundJobsRegistrationTests : IClassFixture<HangfireTestFixture>
{
    public BackgroundJobsRegistrationTests(HangfireTestFixture fixture)
    {
        // Hangfire configuration is handled by HangfireTestFixture
    }


    [Fact]
    public void RegisterRecurringJobs_ShouldNotThrowException()
    {
        // Act & Assert
        var exception = Record.Exception(() => BackgroundJobsRegistration.RegisterRecurringJobs());

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void RegisterRecurringJobs_CanBeCalledMultipleTimes_ShouldNotThrowException()
    {
        // Act & Assert
        var exception1 = Record.Exception(() => BackgroundJobsRegistration.RegisterRecurringJobs());
        var exception2 = Record.Exception(() => BackgroundJobsRegistration.RegisterRecurringJobs());
        var exception3 = Record.Exception(() => BackgroundJobsRegistration.RegisterRecurringJobs());

        // Assert
        Assert.Null(exception1);
        Assert.Null(exception2);
        Assert.Null(exception3);
    }

    [Fact]
    public void RegisterRecurringJobs_IsStaticMethod_ShouldBeCallableWithoutInstance()
    {
        // Arrange & Act
        var method = typeof(BackgroundJobsRegistration)
            .GetMethod("RegisterRecurringJobs", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsStatic);
    }

    [Fact]
    public void RegisterRecurringJobs_IsPublicMethod_ShouldBeAccessible()
    {
        // Arrange & Act
        var method = typeof(BackgroundJobsRegistration)
            .GetMethod("RegisterRecurringJobs", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPublic);
    }
}
