using BusinessLogic.DTOs;
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
        });

        /// <summary>Retrieves a specific additional service by its unique identifier.</summary>
        group.MapGet("/{id:int}", async (int id, IAdditionalServiceService service) =>
        {
            var result = await service.GetByIdAsync(id);

            if (result is not null)
            {
                return Results.Ok(result);
            }

            return Results.NotFound();
        });

        /// <summary>Creates a new additional service.</summary>
        group.MapPost("/", async (CreateAdditionalServiceDto dto, IAdditionalServiceService service) =>
        {
            var createdService = await service.AddAsync(dto);
            return TypedResults.Created($"/api/services/{createdService.Id}", createdService);
        });

        /// <summary>Updates an existing additional service.</summary>
        group.MapPut("/{id:int}", async (int id, UpdateAdditionalServiceDto dto, IAdditionalServiceService service) =>
        {
            if (id != dto.Id)
            {
                return Results.BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            await service.UpdateAsync(dto);
            return TypedResults.NoContent();
        });

        /// <summary>Deletes an additional service by its identifier.</summary>
        group.MapDelete("/{id:int}", async (int id, IAdditionalServiceService service) =>
        {
            await service.DeleteAsync(id);
            return TypedResults.NoContent();
        });
    }
}
