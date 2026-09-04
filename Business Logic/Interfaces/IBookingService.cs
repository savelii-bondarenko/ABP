using DataAccess.Entities;

namespace BusinessLogic.Interfaces;

public interface IBookingService
{
    Task<Booking> AddAsync(Booking booking);

    Task DeleteAsync(int id);

    Task UpdateAsync(Booking booking);

    Task<Booking?> GetByIdAsync(int id);

    Task<IEnumerable<Booking>> GetAllAsync();

    Task<IEnumerable<Booking>> GetOverlappingBookingsAsync(int roomId, DateTime start, DateTime end);
}
