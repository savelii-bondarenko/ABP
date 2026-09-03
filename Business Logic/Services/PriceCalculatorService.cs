namespace BusinessLogic.Services;

public class PriceCalculatorService
{
    public decimal Calculate(decimal basePrice, DateTime startTime, DateTime endTime)
    {
        var durationHours = (decimal)(endTime - startTime).TotalHours;

        return basePrice * durationHours;
    }
}
