using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces;

/// <summary>
/// Defines business logic operations for booking management.
/// </summary>
public interface IBookingService
{
    /// <summary>
    /// Creates a new booking.
    /// </summary>
    /// <param name="createBooking">The data transfer object containing new booking details.</param>
    /// <returns>A DTO containing the created booking details.</returns>
    /// <exception cref="ArgumentNullException">Thrown when createBooking is null.</exception>
    Task<ResponseBookingDto> AddAsync(CreateBookingDto createBooking);

    /// <summary>
    /// Deletes a booking by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the booking to delete.</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Updates an existing booking.
    /// </summary>
    /// <param name="updateBooking">The data transfer object containing updated booking details.</param>
    /// <exception cref="ArgumentNullException">Thrown when updateBooking is null.</exception>
    Task UpdateAsync(UpdateBookingDto updateBooking);

    /// <summary>
    /// Retrieves a specific booking by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the booking.</param>
    /// <returns>A DTO containing booking details, or null if the booking was not found.</returns>
    Task<ResponseBookingDto?> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all bookings in the system.
    /// </summary>
    /// <returns>A collection of booking DTOs.</returns>
    Task<IEnumerable<ResponseBookingDto>> GetAllAsync();

    /// <summary>
    /// Retrieves a list of bookings that overlap with a specific time period for a given room.
    /// </summary>
    /// <param name="roomId">The unique identifier of the room.</param>
    /// <param name="start">The start time to check for overlaps.</param>
    /// <param name="end">The end time to check for overlaps.</param>
    /// <returns>A collection of overlapping booking DTOs.</returns>
    Task<IEnumerable<ResponseBookingDto>> GetOverlappingBookingsAsync(int roomId, DateTime start, DateTime end);
}
