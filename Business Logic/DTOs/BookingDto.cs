namespace BusinessLogic.DTOs;

public record BookingDto(
    int Id,
    int RoomId,
    DateTime StartTime,
    DateTime EndTime,
    decimal TotalPrice
);

public record CreateBookingDto(
    int RoomId,
    DateTime StartTime,
    DateTime EndTime,
    decimal TotalPrice
);

public record UpdateBookingDto(
    int Id,
    int RoomId,
    DateTime StartTime,
    DateTime EndTime,
    decimal TotalPrice
);

public record ResponseBookingDto(
    int Id,
    int RoomId,
    DateTime StartTime,
    DateTime EndTime,
    decimal TotalPrice
);
