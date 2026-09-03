namespace BusinessLogic.Services;

/// <summary>
/// Represents a pricing rule that applies a specific multiplier to the base price during a given time period.
/// </summary>
/// <param name="StartTime">The start time of the day when the rule becomes active.</param>
/// <param name="EndTime">The end time of the day when the rule ceases to be active.</param>
/// <param name="Multiplier">The price multiplier applied during this time period (e.g., 0.9m for a 10% discount, 1.15m for a 15% markup).</param>
public record PricingRule(TimeSpan StartTime, TimeSpan EndTime, decimal Multiplier);

/// <summary>
/// Provides functionality to calculate the total price of a conference room booking 
/// based on dynamic time-of-day pricing rules.
/// </summary>
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

    /// <summary>
    /// Calculates the total price for a booking by applying time-based multipliers to the intersecting booking hours.
    /// </summary>
    /// <param name="basePrice">The standard base price per hour for the room.</param>
    /// <param name="startTime">The exact start date and time of the booking.</param>
    /// <param name="endTime">The exact end date and time of the booking.</param>
    /// <returns>The calculated total price, rounded to two decimal places.</returns>
    /// <remarks>
    /// If the booking spans multiple pricing periods, the price is calculated proportionally for each segment.
    /// Currently, the logic assumes the booking occurs within a single calendar day.
    /// </remarks>
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
