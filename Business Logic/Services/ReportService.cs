using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BusinessLogic.Services;

/// <summary>
/// Provides business logic operations for generating analytical reports.
/// </summary>
/// <param name="bookingRepository">The repository for accessing booking data.</param>
/// <param name="roomRepository">The repository for accessing room data.</param>
/// <param name="mapper">The AutoMapper instance for object mapping.</param>
public class ReportService(
    IBookingRepository bookingRepository,
    IRoomRepository roomRepository,
    IMapper mapper) : IReportService
{
    /// <inheritdoc/>
    public async Task<BusinessReportDto> GetRevenueReportAsync(DateTime startDate, DateTime endDate)
    {
        var allBookings = await bookingRepository.GetAllAsync();
        var allRooms = await roomRepository.GetAllAsync();

        var periodBookings = allBookings
            .Where(b => b.StartTime >= startDate && b.EndTime <= endDate)
            .ToList();

        var roomStats = allRooms.Select(room =>
        {
            var roomBookings = periodBookings.Where(b => b.RoomId == room.Id).ToList();

            var dto = mapper.Map<RoomRevenueDto>(room);

            return dto with
            {
                TotalBookings = roomBookings.Count,
                TotalRevenue = roomBookings.Sum(b => b.TotalPrice)
            };
        })
        .OrderByDescending(r => r.TotalRevenue)
        .ToList();

        var totalRevenue = roomStats.Sum(r => r.TotalRevenue);

        return new BusinessReportDto(startDate, endDate, totalRevenue, roomStats);
    }
}
