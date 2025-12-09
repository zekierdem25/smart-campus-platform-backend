using SmartCampus.API.Models;

namespace SmartCampus.API.Tests.Unit;

public class EmailVerificationTokenTests
{
    #region IsExpired Tests

    [Fact]
    public void IsExpired_WhenExpiresAtIsInPast_ShouldReturnTrue()
    {
        // Arrange
        var token = new EmailVerificationToken
        {
            Token = "test-token",
            ExpiresAt = DateTime.UtcNow.AddHours(-1) // 1 saat önce dolmuş
        };

        // Act
        var result = token.IsExpired;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsExpired_WhenExpiresAtIsInFuture_ShouldReturnFalse()
    {
        // Arrange
        var token = new EmailVerificationToken
        {
            Token = "test-token",
            ExpiresAt = DateTime.UtcNow.AddHours(1) // 1 saat sonra dolacak
        };

        // Act
        var result = token.IsExpired;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsExpired_WhenExpiresAtIsNow_ShouldReturnTrue()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var token = new EmailVerificationToken
        {
            Token = "test-token",
            ExpiresAt = now // Şu an tam olarak dolmuş
        };

        // Act
        var result = token.IsExpired;

        // Assert
        Assert.True(result); // >= operatörü nedeniyle true döner
    }

    #endregion

    #region IsUsed Tests

    [Fact]
    public void IsUsed_WhenUsedAtIsNull_ShouldReturnFalse()
    {
        // Arrange
        var token = new EmailVerificationToken
        {
            Token = "test-token",
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            UsedAt = null
        };

        // Act
        var result = token.IsUsed;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsUsed_WhenUsedAtIsSet_ShouldReturnTrue()
    {
        // Arrange
        var token = new EmailVerificationToken
        {
            Token = "test-token",
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            UsedAt = DateTime.UtcNow
        };

        // Act
        var result = token.IsUsed;

        // Assert
        Assert.True(result);
    }

    #endregion

    #region IsValid Tests

    [Fact]
    public void IsValid_WhenNotExpiredAndNotUsed_ShouldReturnTrue()
    {
        // Arrange
        var token = new EmailVerificationToken
        {
            Token = "test-token",
            ExpiresAt = DateTime.UtcNow.AddHours(1), // Henüz dolmamış
            UsedAt = null // Kullanılmamış
        };

        // Act
        var result = token.IsValid;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValid_WhenExpired_ShouldReturnFalse()
    {
        // Arrange
        var token = new EmailVerificationToken
        {
            Token = "test-token",
            ExpiresAt = DateTime.UtcNow.AddHours(-1), // Süresi dolmuş
            UsedAt = null // Kullanılmamış ama expired
        };

        // Act
        var result = token.IsValid;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_WhenUsed_ShouldReturnFalse()
    {
        // Arrange
        var token = new EmailVerificationToken
        {
            Token = "test-token",
            ExpiresAt = DateTime.UtcNow.AddHours(1), // Henüz dolmamış
            UsedAt = DateTime.UtcNow // Kullanılmış
        };

        // Act
        var result = token.IsValid;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_WhenBothExpiredAndUsed_ShouldReturnFalse()
    {
        // Arrange
        var token = new EmailVerificationToken
        {
            Token = "test-token",
            ExpiresAt = DateTime.UtcNow.AddHours(-1), // Süresi dolmuş
            UsedAt = DateTime.UtcNow.AddHours(-2) // Kullanılmış
        };

        // Act
        var result = token.IsValid;

        // Assert
        Assert.False(result);
    }

    #endregion
}

