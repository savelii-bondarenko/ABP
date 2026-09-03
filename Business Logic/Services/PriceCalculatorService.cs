namespace BusinessLogic.Services;

public record PricingRule(TimeSpan StartTime, TimeSpan EndTime, decimal Multiplier);

public class PriceCalculatorService
{
    private readonly List<PricingRule> _rules = new()
    {
        new PricingRule(new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), 0.9m),  // Sale 10%
        new PricingRule(new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), 1.0m), // Base
        new PricingRule(new TimeSpan(12, 0, 0), new TimeSpan(14, 0, 0), 1.15m), // Rise 15%
        new PricingRule(new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0), 1.0m), // Base
        new PricingRule(new TimeSpan(18, 0, 0), new TimeSpan(23, 0, 0), 0.8m)  // Sale 20%
    };

    public decimal Calculate(decimal basePrice, DateTime startTime, DateTime endTime)
    {
        var bookingDate = startTime.Date;
        decimal totalPrice = 0;

        foreach (var rule in _rules)
        {
            var ruleStart = bookingDate.Add(rule.StartTime);
            var ruleEnd = bookingDate.Add(rule.EndTime);

            var overlapStart = startTime > ruleStart ? startTime : ruleStart;
            var overlapEnd = endTime < ruleEnd ? endTime : ruleEnd;

            if (overlapStart < overlapEnd)
            {
                var overlapHours = (decimal)(overlapEnd - overlapStart).TotalHours;
                totalPrice += basePrice * overlapHours * rule.Multiplier;
            }
        }

        return Math.Round(totalPrice, 2);
    }
}
