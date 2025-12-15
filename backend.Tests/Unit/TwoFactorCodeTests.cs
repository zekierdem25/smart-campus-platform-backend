using SmartCampus.API.Models;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class TwoFactorCodeTests
{
    // ========== IsLocked Tests ==========

    [Fact]
    public void IsLocked_ShouldBeFalse_WhenLockoutEndAtIsNull()
    {
        // Arrange
        var code = new TwoFactorCode { LockoutEndAt = null };

        // Act & Assert
        Assert.False(code.IsLocked);
    }

    [Fact]
    public void IsLocked_ShouldBeTrue_WhenLockoutEndAtIsInFuture()
    {
        // Arrange
        var code = new TwoFactorCode { LockoutEndAt = DateTime.UtcNow.AddMinutes(5) };

        // Act & Assert
        Assert.True(code.IsLocked);
    }

    [Fact]
    public void IsLocked_ShouldBeFalse_WhenLockoutEndAtIsPast()
    {
        // Arrange
        var code = new TwoFactorCode { LockoutEndAt = DateTime.UtcNow.AddMinutes(-5) };

        // Act & Assert
        Assert.False(code.IsLocked);
    }

    // ========== IsValid Tests ==========

    [Fact]
    public void IsValid_ShouldBeTrue_WhenConditionsMet()
    {
        // Arrange
        var code = new TwoFactorCode
        {
            IsUsed = false,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            LockoutEndAt = null
        };

        // Act & Assert
        Assert.True(code.IsValid);
    }

    [Fact]
    public void IsValid_ShouldBeFalse_WhenIsUsedIsTrue()
    {
        // Arrange
        var code = new TwoFactorCode
        {
            IsUsed = true,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            LockoutEndAt = null
        };

        // Act & Assert
        Assert.False(code.IsValid);
    }

    [Fact]
    public void IsValid_ShouldBeFalse_WhenExpired()
    {
        // Arrange
        var code = new TwoFactorCode
        {
            IsUsed = false,
            ExpiresAt = DateTime.UtcNow.AddMinutes(-1),
            LockoutEndAt = null
        };

        // Act & Assert
        Assert.False(code.IsValid);
    }

    [Fact]
    public void IsValid_ShouldBeFalse_WhenLocked()
    {
        // Arrange
        // (LockoutEndAt in future => IsLocked=true => IsValid should be false)
        var code = new TwoFactorCode
        {
            IsUsed = false,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5),
            LockoutEndAt = DateTime.UtcNow.AddMinutes(10)
        };

        // Act & Assert (Logic check: IsValid => !IsUsed && ExpiresAt > UtcNow && LockoutEndAt == null)
        Assert.False(code.IsValid); 
    }
}
