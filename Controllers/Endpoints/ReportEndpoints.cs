using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Controllers.Endpoints;

/// <summary>
/// Configures routing endpoints for business analytics and reports.
/// </summary>
public static class ReportEndpoints
{
    public static void MapReportEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/reports").WithTags("Reports");

        /// <summary>Generates a business report showing revenue and popular rooms for a given period.</summary>
        group.MapGet("/revenue", async (
            [FromQuery] DateTime start,
            [FromQuery] DateTime end,
            IReportService reportService) =>
        {
            var report = await reportService.GetRevenueReportAsync(start, end);
            return TypedResults.Ok(report);
        })
        .WithName("GetRevenueReport");
    }
}
