using DataAccess.Entities;

namespace DataAccess.Interfaces;

public interface IRoomRepository
{
    Task<Room> AddAsync(Room room);

    Task<Room?> GetByIdAsync(int id);

    Task<IEnumerable<Room>> GetAllAsync();

    Task UpdateAsync(Room room);

    Task DeleteAsync(int id);

    Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime, int capacity);
}
