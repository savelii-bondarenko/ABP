using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Endpoints;

/// <summary>
/// Configures routing endpoints for conference rooms management.
/// </summary>
public static class RoomEndpoints
{
    public static void MapRoomEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/rooms").WithTags("Rooms");

        /// <summary>Retrieves all conference rooms.</summary>
        group.MapGet("/", async (IRoomService roomService) =>
        {
            var rooms = await roomService.GetAllAsync();
            return TypedResults.Ok(rooms);
        });

        /// <summary>Retrieves a specific room by its unique identifier.</summary>
        group.MapGet("/{id:int}", async Task<Results<Ok<ResponseRoomDto>, NotFound>> (int id, IRoomService roomService) =>
        {
            var room = await roomService.GetByIdAsync(id);
            if (room is not null)
            {
                return TypedResults.Ok(room);
            }

            return TypedResults.NotFound();
        });

        /// <summary>Searches for available rooms within a specified timeframe and capacity.</summary>
        group.MapGet("/available", async (
            [FromQuery] DateTime start,
            [FromQuery] DateTime end,
            [FromQuery] int capacity,
            IRoomService roomService) =>
        {
            var rooms = await roomService.GetAvailableRoomsAsync(start, end, capacity);
            return TypedResults.Ok(rooms);
        });

        /// <summary>Creates a new conference room.</summary>
        group.MapPost("/", async (CreateRoomDto dto, IRoomService roomService) =>
        {
            var createdRoom = await roomService.AddAsync(dto);
            return TypedResults.Created($"/api/rooms/{createdRoom.Id}", createdRoom);
        });

        /// <summary>Updates an existing conference room.</summary>
        group.MapPut("/{id:int}", async (int id, UpdateRoomDto dto, IRoomService roomService) =>
        {
            await roomService.UpdateAsync(dto);
            return TypedResults.NoContent();
        });

        /// <summary>Deletes a conference room by its identifier.</summary>
        group.MapDelete("/{id:int}", async (int id, IRoomService roomService) =>
        {
            await roomService.DeleteAsync(id);
            return TypedResults.NoContent();
        });
    }
}
