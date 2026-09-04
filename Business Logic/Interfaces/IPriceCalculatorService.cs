namespace BusinessLogic.Interfaces;

/// <summary>
/// Defines a service for calculating the total price of a booking.
/// </summary>
public interface IPriceCalculatorService
{
    /// <summary>
    /// Calculates the total price for a booking based on the room's base price and the specified time period.
    /// </summary>
    /// <param name="basePricePerHour">The standard base price per hour for the room.</param>
    /// <param name="startTime">The exact start date and time of the booking.</param>
    /// <param name="endTime">The exact end date and time of the booking.</param>
    /// <returns>The calculated total price.</returns>
    decimal Calculate(decimal basePricePerHour, DateTime startTime, DateTime endTime);
}
