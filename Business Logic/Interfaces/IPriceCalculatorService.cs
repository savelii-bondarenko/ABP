namespace BusinessLogic.Interfaces;

public interface IPriceCalculatorService
{
    decimal Calculate(decimal basePricePerHour, DateTime startTime, DateTime endTime);
}