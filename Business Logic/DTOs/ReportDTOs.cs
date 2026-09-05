namespace BusinessLogic.DTOs;

/// <summary>
/// Contains statistics for a specific conference room.
/// </summary>
public record RoomRevenueDto(
    int RoomId,
    string RoomName,
    int TotalBookings,
    decimal TotalRevenue
);

/// <summary>
/// Represents a comprehensive business report for a given period.
/// </summary>
public record BusinessReportDto(
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalRevenue,
    IEnumerable<RoomRevenueDto> RoomStatistics
);
