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
}
