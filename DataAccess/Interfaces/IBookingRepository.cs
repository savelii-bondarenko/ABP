using DataAccess.Entities;

namespace DataAccess.Interfaces;

/// <summary>
/// Defines the repository contract for managing Booking entities in the database.
/// </summary>
public interface IBookingRepository
{
    /// <summary>
    /// Adds a new booking to the database.
    /// </summary>
    /// <param name="booking">The booking entity to add.</param>
    /// <returns>The added booking entity with its generated unique identifier.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the booking parameter is null.</exception>
    Task<Booking> AddAsync(Booking booking);

    /// <summary>
    /// Deletes a booking from the database by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the booking to delete.</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Updates an existing booking in the database.
    /// </summary>
    /// <param name="booking">The booking entity containing updated data.</param>
    /// <exception cref="ArgumentNullException">Thrown when the booking parameter is null.</exception>
    Task UpdateAsync(Booking booking);

    /// <summary>
    /// Retrieves a specific booking by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the booking.</param>
    /// <returns>The booking entity if found; otherwise, null.</returns>
    Task<Booking?> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all bookings from the database.
    /// </summary>
    /// <returns>A read-only collection of all booking entities.</returns>
    Task<IEnumerable<Booking>> GetAllAsync();

    /// <summary>
    /// Retrieves a list of bookings for a specific room that overlap with the provided time range.
    /// </summary>
    /// <param name="roomId">The unique identifier of the room.</param>
    /// <param name="start">The proposed start time of the new booking.</param>
    /// <param name="end">The proposed end time of the new booking.</param>
    /// <returns>A collection of overlapping bookings, if any exist.</returns>
    Task<IEnumerable<Booking>> GetOverlappingBookingsAsync(int roomId, DateTime start, DateTime end);
}