using SmartCampus.API.Controllers;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class PaymentWebhookDtoTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var paymentId = "pay_123456789";
        var status = "completed";
        var amount = 100.50m;
        var userId = Guid.NewGuid();
        var signature = "signature_hash_abc123";

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            status,
            amount,
            userId,
            signature
        );

        // Assert
        Assert.Equal(paymentId, dto.PaymentId);
        Assert.Equal(status, dto.Status);
        Assert.Equal(amount, dto.Amount);
        Assert.Equal(userId, dto.UserId);
        Assert.Equal(signature, dto.Signature);
    }

    [Fact]
    public void Constructor_WithNullOptionalParameters_ShouldSetNullsCorrectly()
    {
        // Arrange
        var paymentId = "pay_987654321";
        var status = "pending";
        var amount = 250.75m;

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            status,
            amount,
            null, // UserId
            null  // Signature
        );

        // Assert
        Assert.Equal(paymentId, dto.PaymentId);
        Assert.Equal(status, dto.Status);
        Assert.Equal(amount, dto.Amount);
        Assert.Null(dto.UserId);
        Assert.Null(dto.Signature);
    }

    [Fact]
    public void Constructor_WithEmptyPaymentId_ShouldSetEmptyString()
    {
        // Arrange
        var status = "failed";
        var amount = 0m;

        // Act
        var dto = new PaymentWebhookDto(
            string.Empty,
            status,
            amount,
            null,
            null
        );

        // Assert
        Assert.Equal(string.Empty, dto.PaymentId);
        Assert.Equal(status, dto.Status);
        Assert.Equal(amount, dto.Amount);
    }

    [Fact]
    public void Constructor_WithEmptyStatus_ShouldSetEmptyString()
    {
        // Arrange
        var paymentId = "pay_test";
        var amount = 50.25m;

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            string.Empty,
            amount,
            null,
            null
        );

        // Assert
        Assert.Equal(paymentId, dto.PaymentId);
        Assert.Equal(string.Empty, dto.Status);
        Assert.Equal(amount, dto.Amount);
    }

    [Fact]
    public void Constructor_WithDifferentStatusValues_ShouldSetCorrectly()
    {
        // Arrange
        var paymentId = "pay_status_test";
        var amount = 100m;
        var statuses = new[] { "pending", "completed", "failed", "refunded", "cancelled" };

        // Act & Assert
        foreach (var status in statuses)
        {
            var dto = new PaymentWebhookDto(paymentId, status, amount, null, null);
            Assert.Equal(status, dto.Status);
        }
    }

    [Fact]
    public void Constructor_WithZeroAmount_ShouldSetZero()
    {
        // Arrange
        var paymentId = "pay_zero";
        var status = "completed";

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            status,
            0m,
            null,
            null
        );

        // Assert
        Assert.Equal(0m, dto.Amount);
    }

    [Fact]
    public void Constructor_WithNegativeAmount_ShouldSetNegativeValue()
    {
        // Arrange
        var paymentId = "pay_negative";
        var status = "refunded";

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            status,
            -100.50m,
            null,
            null
        );

        // Assert
        Assert.Equal(-100.50m, dto.Amount);
    }

    [Fact]
    public void Constructor_WithLargeAmount_ShouldSetLargeValue()
    {
        // Arrange
        var paymentId = "pay_large";
        var status = "completed";

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            status,
            999999.99m,
            null,
            null
        );

        // Assert
        Assert.Equal(999999.99m, dto.Amount);
    }

    [Fact]
    public void Constructor_WithDecimalAmount_ShouldSetCorrectly()
    {
        // Arrange
        var paymentId = "pay_decimal";
        var status = "completed";
        var amounts = new[] { 0.01m, 0.99m, 1.50m, 99.99m, 100.50m, 1000.25m };

        // Act & Assert
        foreach (var amount in amounts)
        {
            var dto = new PaymentWebhookDto(paymentId, status, amount, null, null);
            Assert.Equal(amount, dto.Amount);
        }
    }

    [Fact]
    public void Constructor_WithUserId_ShouldSetUserIdCorrectly()
    {
        // Arrange
        var paymentId = "pay_with_user";
        var status = "completed";
        var amount = 200m;
        var userId = Guid.NewGuid();

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            status,
            amount,
            userId,
            null
        );

        // Assert
        Assert.Equal(userId, dto.UserId);
        Assert.NotNull(dto.UserId);
    }

    [Fact]
    public void Constructor_WithDifferentUserIds_ShouldSetCorrectly()
    {
        // Arrange
        var paymentId = "pay_multi_user";
        var status = "completed";
        var amount = 150m;
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();

        // Act
        var dto1 = new PaymentWebhookDto(paymentId, status, amount, userId1, null);
        var dto2 = new PaymentWebhookDto(paymentId, status, amount, userId2, null);

        // Assert
        Assert.Equal(userId1, dto1.UserId);
        Assert.Equal(userId2, dto2.UserId);
        Assert.NotEqual(dto1.UserId, dto2.UserId);
    }

    [Fact]
    public void Constructor_WithSignature_ShouldSetSignatureCorrectly()
    {
        // Arrange
        var paymentId = "pay_signed";
        var status = "completed";
        var amount = 300m;
        var signature = "sha256_hash_signature_12345";

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            status,
            amount,
            null,
            signature
        );

        // Assert
        Assert.Equal(signature, dto.Signature);
        Assert.NotNull(dto.Signature);
    }

    [Fact]
    public void Constructor_WithLongStrings_ShouldSetCorrectly()
    {
        // Arrange
        var longPaymentId = new string('A', 200);
        var longStatus = new string('B', 100);
        var longSignature = new string('C', 500);
        var amount = 100m;

        // Act
        var dto = new PaymentWebhookDto(
            longPaymentId,
            longStatus,
            amount,
            null,
            longSignature
        );

        // Assert
        Assert.Equal(longPaymentId, dto.PaymentId);
        Assert.Equal(longStatus, dto.Status);
        Assert.Equal(longSignature, dto.Signature);
        Assert.Equal(200, dto.PaymentId.Length);
        Assert.Equal(100, dto.Status.Length);
        Assert.Equal(500, dto.Signature?.Length);
    }

    [Fact]
    public void Constructor_WithEmptyGuid_ShouldSetEmptyGuid()
    {
        // Arrange
        var paymentId = "pay_empty_guid";
        var status = "completed";
        var amount = 50m;
        var emptyGuid = Guid.Empty;

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            status,
            amount,
            emptyGuid,
            null
        );

        // Assert
        Assert.Equal(emptyGuid, dto.UserId);
        Assert.Equal(Guid.Empty, dto.UserId);
    }

    [Fact]
    public void Constructor_WithOnlyRequiredParameters_ShouldSetCorrectly()
    {
        // Arrange
        var paymentId = "pay_required_only";
        var status = "pending";
        var amount = 75.50m;

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            status,
            amount,
            null,
            null
        );

        // Assert
        Assert.Equal(paymentId, dto.PaymentId);
        Assert.Equal(status, dto.Status);
        Assert.Equal(amount, dto.Amount);
        Assert.Null(dto.UserId);
        Assert.Null(dto.Signature);
    }

    [Fact]
    public void Constructor_WithOnlyUserId_ShouldSetUserIdOnly()
    {
        // Arrange
        var paymentId = "pay_user_only";
        var status = "completed";
        var amount = 100m;
        var userId = Guid.NewGuid();

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            status,
            amount,
            userId,
            null
        );

        // Assert
        Assert.Equal(userId, dto.UserId);
        Assert.Null(dto.Signature);
    }

    [Fact]
    public void Constructor_WithOnlySignature_ShouldSetSignatureOnly()
    {
        // Arrange
        var paymentId = "pay_signature_only";
        var status = "completed";
        var amount = 100m;
        var signature = "test_signature";

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            status,
            amount,
            null,
            signature
        );

        // Assert
        Assert.Null(dto.UserId);
        Assert.Equal(signature, dto.Signature);
    }

    [Fact]
    public void RecordEquality_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var paymentId = "pay_equal";
        var status = "completed";
        var amount = 100m;
        var userId = Guid.NewGuid();
        var signature = "signature123";

        // Act
        var dto1 = new PaymentWebhookDto(paymentId, status, amount, userId, signature);
        var dto2 = new PaymentWebhookDto(paymentId, status, amount, userId, signature);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentPaymentIds_ShouldNotBeEqual()
    {
        // Arrange
        var status = "completed";
        var amount = 100m;

        // Act
        var dto1 = new PaymentWebhookDto("pay_1", status, amount, null, null);
        var dto2 = new PaymentWebhookDto("pay_2", status, amount, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentStatuses_ShouldNotBeEqual()
    {
        // Arrange
        var paymentId = "pay_same";
        var amount = 100m;

        // Act
        var dto1 = new PaymentWebhookDto(paymentId, "completed", amount, null, null);
        var dto2 = new PaymentWebhookDto(paymentId, "pending", amount, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentAmounts_ShouldNotBeEqual()
    {
        // Arrange
        var paymentId = "pay_same";
        var status = "completed";

        // Act
        var dto1 = new PaymentWebhookDto(paymentId, status, 100m, null, null);
        var dto2 = new PaymentWebhookDto(paymentId, status, 200m, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentUserIds_ShouldNotBeEqual()
    {
        // Arrange
        var paymentId = "pay_same";
        var status = "completed";
        var amount = 100m;

        // Act
        var dto1 = new PaymentWebhookDto(paymentId, status, amount, Guid.NewGuid(), null);
        var dto2 = new PaymentWebhookDto(paymentId, status, amount, Guid.NewGuid(), null);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithNullAndNotNullUserId_ShouldNotBeEqual()
    {
        // Arrange
        var paymentId = "pay_same";
        var status = "completed";
        var amount = 100m;
        var userId = Guid.NewGuid();

        // Act
        var dto1 = new PaymentWebhookDto(paymentId, status, amount, null, null);
        var dto2 = new PaymentWebhookDto(paymentId, status, amount, userId, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentSignatures_ShouldNotBeEqual()
    {
        // Arrange
        var paymentId = "pay_same";
        var status = "completed";
        var amount = 100m;

        // Act
        var dto1 = new PaymentWebhookDto(paymentId, status, amount, null, "signature1");
        var dto2 = new PaymentWebhookDto(paymentId, status, amount, null, "signature2");

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithNullAndNotNullSignature_ShouldNotBeEqual()
    {
        // Arrange
        var paymentId = "pay_same";
        var status = "completed";
        var amount = 100m;
        var signature = "test_signature";

        // Act
        var dto1 = new PaymentWebhookDto(paymentId, status, amount, null, null);
        var dto2 = new PaymentWebhookDto(paymentId, status, amount, null, signature);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void Constructor_WithVerySmallAmount_ShouldSetCorrectly()
    {
        // Arrange
        var paymentId = "pay_small";
        var status = "completed";
        var verySmallAmount = 0.01m;

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            status,
            verySmallAmount,
            null,
            null
        );

        // Assert
        Assert.Equal(verySmallAmount, dto.Amount);
    }

    [Fact]
    public void Constructor_WithVeryLargeAmount_ShouldSetCorrectly()
    {
        // Arrange
        var paymentId = "pay_large";
        var status = "completed";
        var veryLargeAmount = 999999999.99m;

        // Act
        var dto = new PaymentWebhookDto(
            paymentId,
            status,
            veryLargeAmount,
            null,
            null
        );

        // Assert
        Assert.Equal(veryLargeAmount, dto.Amount);
    }
}

