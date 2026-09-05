using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;

namespace Controllers.Endpoints;

/// <summary>
/// Configures routing endpoints for booking operations.
/// </summary>
public static class BookingEndpoints
{
    public static void MapBookingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/bookings").WithTags("Bookings");

        /// <summary>Retrieves all bookings.</summary>
        group.MapGet("/", async (IBookingService bookingService) =>
        {
            var bookings = await bookingService.GetAllAsync();
            return TypedResults.Ok(bookings);
        });

        /// <summary>Retrieves a specific booking by its unique identifier.</summary>
        group.MapGet("/{id:int}", async (int id, IBookingService bookingService) =>
        {
            var booking = await bookingService.GetByIdAsync(id);
            return booking is not null ? Results.Ok(booking) : Results.NotFound();
        });

        /// <summary>Creates a new booking, ensuring no overlapping time conflicts.</summary>
        group.MapPost("/", async (CreateBookingDto dto, IBookingService bookingService) =>
        {
            try
            {
                var createdBooking = await bookingService.AddAsync(dto);
                return Results.Created($"/api/bookings/{createdBooking.Id}", createdBooking);
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        /// <summary>Updates an existing booking.</summary>
        group.MapPut("/{id:int}", async (int id, UpdateBookingDto dto, IBookingService bookingService) =>
        {
            if (id != dto.Id)
            {
                return Results.BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            await bookingService.UpdateAsync(dto);
            return Results.NoContent();
        });

        /// <summary>Deletes a booking by its identifier.</summary>
        group.MapDelete("/{id:int}", async (int id, IBookingService bookingService) =>
        {
            await bookingService.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}
