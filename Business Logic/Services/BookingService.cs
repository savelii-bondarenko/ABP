using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services;

/// <summary>
/// Provides business logic operations for managing bookings.
/// </summary>
/// <param name="repository">The booking repository for data access.</param>
/// <param name="mapper">The AutoMapper instance for object mapping.</param>
public class BookingService(IBookingRepository repository, IMapper mapper) : IBookingService
{
    /// <inheritdoc/>
    public async Task<ResponseBookingDto> AddAsync(CreateBookingDto createBooking)
    {
        ArgumentNullException.ThrowIfNull(createBooking);

        Booking booking = mapper.Map<Booking>(createBooking);
        var result = await repository.AddAsync(booking);

        return mapper.Map<ResponseBookingDto>(result);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ResponseBookingDto>> GetAllAsync()
    {
        var bookings = await repository.GetAllAsync();

        return mapper.Map<IEnumerable<ResponseBookingDto>>(bookings);
    }

    /// <inheritdoc/>
    public async Task<ResponseBookingDto?> GetByIdAsync(int id)
    {
        var booking = await repository.GetByIdAsync(id);

        if (booking is null)
        {
            return null;
        }

        return mapper.Map<ResponseBookingDto>(booking);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ResponseBookingDto>> GetOverlappingBookingsAsync(int roomId, DateTime start, DateTime end)
    {
        var bookings = await repository.GetOverlappingBookingsAsync(roomId, start, end);

        return mapper.Map<IEnumerable<ResponseBookingDto>>(bookings);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(UpdateBookingDto updateBooking)
    {
        ArgumentNullException.ThrowIfNull(updateBooking);

        Booking booking = mapper.Map<Booking>(updateBooking);

        await repository.UpdateAsync(booking);
    }
}
