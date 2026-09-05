using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces;

/// <summary>
/// Defines business logic operations for conference rooms management.
/// </summary>
public interface IRoomService
{
    /// <summary>
    /// Adds a new conference room.
    /// </summary>
    /// <param name="roomDto">The data transfer object containing new room details.</param>
    /// <returns>A DTO containing the created room details.</returns>
    /// <exception cref="ArgumentNullException">Thrown when roomDto is null.</exception>
    Task<ResponseRoomDto> AddAsync(CreateRoomDto roomDto);

    /// <summary>
    /// Retrieves a specific room by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the room.</param>
    /// <returns>A DTO containing room details, or null if the room was not found.</returns>
    Task<ResponseRoomDto?> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all available conference rooms in the system.
    /// </summary>
    /// <returns>A collection of room DTOs.</returns>
    Task<IEnumerable<ResponseRoomDto>> GetAllAsync();

    /// <summary>
    /// Updates an existing conference room.
    /// </summary>
    /// <param name="roomDto">The data transfer object containing updated room details.</param>
    /// <exception cref="ArgumentNullException">Thrown when roomDto is null.</exception>
    Task UpdateAsync(UpdateRoomDto roomDto);

    /// <summary>
    /// Deletes a conference room by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the room to delete.</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Retrieves a list of rooms that are available for a specific time period and meet the capacity requirements.
    /// </summary>
    /// <param name="startTime">The desired start time of the booking.</param>
    /// <param name="endTime">The desired end time of the booking.</param>
    /// <param name="capacity">The minimum required capacity of the room.</param>
    /// <returns>A collection of available room DTOs.</returns>
    Task<IEnumerable<ResponseRoomDto>> GetAvailableRoomsAsync(DateTime startTime, DateTime endTime, int capacity);
}
