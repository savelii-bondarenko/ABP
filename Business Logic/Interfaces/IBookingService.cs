using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces;

public interface IBookingService
{
    Task<ResponseBookingDto> AddAsync(CreateBookingeDto createBooking);

    Task DeleteAsync(int id);

    Task UpdateAsync(UpdateBookingDto updateBooking);

    Task<ResponseBookingDto?> GetByIdAsync(int id);

    Task<IEnumerable<ResponseBookingDto>> GetAllAsync();

    Task<IEnumerable<ResponseBookingDto>> GetOverlappingBookingsAsync(int roomId, DateTime start, DateTime end);
}
