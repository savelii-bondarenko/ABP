using BusinessLogic.Interfaces;

namespace Controllers.Endpoints;

/// <summary>
/// Configures routing endpoints for additional services.
/// </summary>
public static class AdditionalServiceEndpoints
{
    public static void MapAdditionalServiceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/services").WithTags("Additional Services");

        /// <summary>Retrieves all available additional services.</summary>
        group.MapGet("/", async (IAdditionalServiceService service) =>
        {
            var services = await service.GetAllAsync();
            return TypedResults.Ok(services);
        })
        .WithName("GetAllServices");
    }
}
