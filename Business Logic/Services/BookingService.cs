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
public class BookingService(
    IBookingRepository bookingRepository,
    IRoomRepository roomRepository,
    IAdditionalServiceRepository additionalServiceRepository,
    IMapper mapper,
    IPriceCalculatorService calculatorService) : IBookingService
{
    /// <inheritdoc/>
    public async Task<ResponseBookingDto> AddAsync(CreateBookingDto createBooking)
    {
        ArgumentNullException.ThrowIfNull(createBooking);

        var overlappingBookings = await bookingRepository.GetOverlappingBookingsAsync(
            createBooking.RoomId, createBooking.StartTime, createBooking.EndTime);

        if (overlappingBookings.Any())
        {
            throw new InvalidOperationException("The room is already booked for the selected time period.");
        }

        var room = await roomRepository.GetByIdAsync(createBooking.RoomId);
        if (room == null)
        {
            throw new InvalidOperationException($"Room with ID {createBooking.RoomId} not found.");
        }

        var roomRentPrice = calculatorService.Calculate(room.BasePricePerHour, createBooking.StartTime, createBooking.EndTime);

        decimal additionalServicesPrice = 0;
        var selectedServices = new List<AdditionalService>();

        if (createBooking.AdditionalServiceIds != null && createBooking.AdditionalServiceIds.Any())
        {
            var services = await additionalServiceRepository.GetByIdsAsync(createBooking.AdditionalServiceIds);
            selectedServices = services.ToList();
            additionalServicesPrice = selectedServices.Sum(s => s.Price);
        }

        Booking booking = mapper.Map<Booking>(createBooking);

        booking.TotalPrice = roomRentPrice + additionalServicesPrice;

        booking.SelectedServices = selectedServices;

        var result = await bookingRepository.AddAsync(booking);

        return mapper.Map<ResponseBookingDto>(result);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id)
    {
        await bookingRepository.DeleteAsync(id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ResponseBookingDto>> GetAllAsync()
    {
        var bookings = await bookingRepository.GetAllAsync();

        return mapper.Map<IEnumerable<ResponseBookingDto>>(bookings);
    }

    /// <inheritdoc/>
    public async Task<ResponseBookingDto?> GetByIdAsync(int id)
    {
        var booking = await bookingRepository.GetByIdAsync(id);

        if (booking is null)
        {
            return null;
        }

        return mapper.Map<ResponseBookingDto>(booking);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ResponseBookingDto>> GetOverlappingBookingsAsync(int roomId, DateTime start, DateTime end)
    {
        var bookings = await bookingRepository.GetOverlappingBookingsAsync(roomId, start, end);

        return mapper.Map<IEnumerable<ResponseBookingDto>>(bookings);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(UpdateBookingDto updateBooking)
    {
        ArgumentNullException.ThrowIfNull(updateBooking);

        Booking booking = mapper.Map<Booking>(updateBooking);

        await bookingRepository.UpdateAsync(booking);
    }
}
