using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class RoomRepository(AppDbContext context) : IRoomRepository
{
    public async Task<Room> AddAsync(Room room)
    {
        ArgumentNullException.ThrowIfNull(room);

        await context.Rooms.AddAsync(room);

        await context.SaveChangesAsync();

        return room;
    }

    public async Task DeleteAsync(int id)
    {
        await context.Rooms.Where(r => r.Id == id).ExecuteDeleteAsync();
    }

    public async Task UpdateAsync(Room room)
    {
        ArgumentNullException.ThrowIfNull(room);

        context.Rooms.Update(room);

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await context.Rooms.AsNoTracking().ToListAsync();
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await context.Rooms.FindAsync(id);
    }

    public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime, int capacity)
    {
        return await context.Rooms
            .AsNoTracking()
            .Where(r => r.Capacity >= capacity)
            .Where(r => !r.Bookings.Any(b => b.StartTime < endTime && b.EndTime > startTime))
            .ToListAsync();
    }
}
