using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;

namespace BusinessLogic.Services;

public class ReportService(
    IBookingRepository bookingRepository,
    IRoomRepository roomRepository) : IReportService
{
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

            return new RoomRevenueDto(
                room.Id,
                room.Name,
                roomBookings.Count,
                roomBookings.Sum(b => b.TotalPrice)
            );
        })
        .OrderByDescending(r => r.TotalRevenue)
        .ToList();

        var totalRevenue = roomStats.Sum(r => r.TotalRevenue);

        return new BusinessReportDto(startDate, endDate, totalRevenue, roomStats);
    }
}
