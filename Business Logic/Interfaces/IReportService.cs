using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces;

/// <summary>
/// Defines business logic for generating analytical reports.
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Generates a report containing revenue and room popularity statistics for a specific time period.
    /// </summary>
    Task<BusinessReportDto> GetRevenueReportAsync(DateTime startDate, DateTime endDate);
}
