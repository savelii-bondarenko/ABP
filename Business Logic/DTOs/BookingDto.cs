namespace BusinessLogic.DTOs;

public record BookingDto(
    int Id,
    int RoomId,
    DateTime StartTime,
    DateTime EndTime,
    decimal TotalPrice
);

public record CreatBookingeDto(
    int RoomId,
    DateTime StartTime,
    DateTime EndTime,
    decimal TotalPrice
);

public record UpdateBookingDto(
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
