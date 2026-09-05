using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

/// <summary>
/// Repository class for the Room entity. Handles all direct database operations.
/// </summary>
/// <param name="context">The application database context.</param>
public class RoomRepository(AppDbContext context) : IRoomRepository
{
    /// <inheritdoc/>
    public async Task<Room> AddAsync(Room room)
    {
        ArgumentNullException.ThrowIfNull(room);

        await context.Rooms.AddAsync(room);
        await context.SaveChangesAsync();

        return room;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id)
    {
        await context.Rooms.Where(r => r.Id == id).ExecuteDeleteAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Room room)
    {
        ArgumentNullException.ThrowIfNull(room);

        context.Rooms.Update(room);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await context.Rooms.AsNoTracking().ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<Room?> GetByIdAsync(int id)
    {
        return await context.Rooms.FindAsync(id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime, int capacity)
    {
        return await context.Rooms
            .AsNoTracking()
            .Where(r => r.Capacity >= capacity)
            // Ensures the room has no booking where the times intersect with the requested timeframe
            .Where(r => !r.Bookings.Any(b => b.StartTime < endTime && b.EndTime > startTime))
            .ToListAsync();
    }
}
