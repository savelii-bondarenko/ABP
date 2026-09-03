using BusinessLogic.Services;

namespace BusinessLogic.Tests;

public class PriceCalculatorServiceTests
{
    [Fact]
    public void CalculatePrice_StandardHours_ReturnsBasePrice()
    {
        // Arrange
        var calculator = new PriceCalculatorService();
        var basePrice = 2000m;

        var startTime = new DateTime(2024, 9, 1, 10, 0, 0);
        var endTime = new DateTime(2024, 9, 1, 12, 0, 0);

        // Act
        var result = calculator.Calculate(basePrice, startTime, endTime);

        // Assert
        Assert.Equal(4000m, result);
    }

    [Fact]
    public void CalculatePrice_EveningHours_ReturnsSale20Percent()
    {
        // Arrange
        var calculator = new PriceCalculatorService();
        var basePrice = 2000m;

        var startTime = new DateTime(2024, 9, 1, 19, 0, 0);
        var endTime = new DateTime(2024, 9, 1, 21, 0, 0);

        // Act
        var result = calculator.Calculate(basePrice, startTime, endTime);

        // Assert
        Assert.Equal(3200m, result);
    }

    [Fact]
    public void CalculatePrice_MorningHours_ReturnsSale10Percent()
    {
        // Arrange
        var calculator = new PriceCalculatorService();
        var basePrice = 2000m;

        var startTime = new DateTime(2024, 9, 1, 7, 0, 0);
        var endTime = new DateTime(2024, 9, 1, 8, 0, 0);

        // Act
        var result = calculator.Calculate(basePrice, startTime, endTime);

        // Assert
        Assert.Equal(1800, result);
    }

    [Fact]
    public void CalculatePrice_HotHours_ReturnsRising15Percent()
    {
        // Arrange
        var calculator = new PriceCalculatorService();
        var basePrice = 2000m;

        var startTime = new DateTime(2024, 9, 1, 12, 0, 0);
        var endTime = new DateTime(2024, 9, 1, 13, 0, 0);

        // Act
        var result = calculator.Calculate(basePrice, startTime, endTime);

        // Assert
        Assert.Equal(2300, result);
    }

    [Theory]
    // 1 hour Base (11:00-12:00) + 1 hour Rise 15% (12:00-13:00)
    // (2000 * 1) + (2000 * 1.15) = 2000 + 2300 = 4300
    [InlineData("2024-09-01T11:00:00", "2024-09-01T13:00:00", 2000, 4300)]

    // 0.5 hours Base (17:30-18:00) + 1.5 hours Sale 20% (18:00-19:30)
    // (2000 * 0.5) + (2000 * 1.5 * 0.8) = 1000 + 2400 = 3400
    [InlineData("2024-09-01T17:30:00", "2024-09-01T19:30:00", 2000, 3400)]

    // 1 hour Sale 10% (08:00-09:00) + 3 hours Base (09:00-12:00) + 1 hour Rise 15% (12:00-13:00)
    // 1800 + 6000 + 2300 = 10100
    [InlineData("2024-09-01T08:00:00", "2024-09-01T13:00:00", 2000, 10100)]

    // Full day booking across all pricing zones (06:00 to 23:00)
    // 3h(0.9) + 3h(1.0) + 2h(1.15) + 4h(1.0) + 5h(0.8) -> 2000 * 16.0 multiplier total = 32000
    [InlineData("2024-09-01T06:00:00", "2024-09-01T23:00:00", 2000, 32000)]

    // Fractional hours edge case: 15 minutes in Base zone (09:00-09:15)
    // 2000 * 0.25 = 500
    [InlineData("2024-09-01T09:00:00", "2024-09-01T09:15:00", 2000, 500)]
    public void CalculatePrice_MixedZones_ReturnsCorrectTotalPrice(
        string startTimeString,
        string endTimeString,
        decimal basePrice,
        decimal expectedTotalPrice)
    {
        // Arrange
        var calculator = new PriceCalculatorService();
        var startTime = DateTime.Parse(startTimeString);
        var endTime = DateTime.Parse(endTimeString);

        // Act
        var result = calculator.Calculate(basePrice, startTime, endTime);

        // Assert
        Assert.Equal(expectedTotalPrice, result);
    }
}
