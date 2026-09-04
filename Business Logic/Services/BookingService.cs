using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BusinessLogic.Services;

public class BookingService(IBookingRepository repository, IMapper mapper) : IBookingService
{
    public Task<ResponseBookingDto> AddAsync(CreateBookingeDto createBooking)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ResponseBookingDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseBookingDto?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ResponseBookingDto>> GetOverlappingBookingsAsync(int roomId, DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UpdateBookingDto updateBooking)
    {
        throw new NotImplementedException();
    }
}
