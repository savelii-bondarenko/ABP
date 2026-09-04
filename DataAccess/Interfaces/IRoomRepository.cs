using DataAccess.Entities;

namespace DataAccess.Interfaces;

/// <summary>
/// Interface was created for working with Room Entitie's repository pattern.
/// </summary>
public interface IRoomRepository
{
    /// <summary>
    /// Adds a new room to the database and saves changes.
    /// </summary>
    /// <param name="room">The room entity to add.</param>
    /// <returns>The added room entity with generated database properties (like Id).</returns>
    /// <exception cref="ArgumentNullException">Thrown when the room entity is null.</exception>
    Task<Room> AddAsync(Room room);

    /// <summary>
    /// Retrieves a specific room by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the room.</param>
    /// <returns>The room entity if found; otherwise, null.</returns>
    Task<Room?> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all rooms from the database without tracking.
    /// </summary>
    /// <returns>A collection of all room entities.</returns>
    Task<IEnumerable<Room>> GetAllAsync();

    /// <summary>
    /// Updates an existing room in the database.
    /// </summary>
    /// <param name="room">The room entity with updated values.</param>
    /// <exception cref="ArgumentNullException">Thrown when the room entity is null.</exception>
    Task UpdateAsync(Room room);

    /// <summary>
    /// Deletes a room from the database efficiently using ExecuteDeleteAsync.
    /// </summary>
    /// <param name="id">The unique identifier of the room to delete.</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Retrieves a list of rooms that have sufficient capacity and no overlapping bookings for the specified timeframe.
    /// </summary>
    /// <param name="startTime">The desired start time.</param>
    /// <param name="endTime">The desired end time.</param>
    /// <param name="capacity">The required capacity.</param>
    /// <returns>A collection of available room entities.</returns>
    Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime, int capacity);
}
