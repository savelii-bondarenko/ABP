namespace BusinessLogic.DTOs;

public record RoomDto(
    int Id,
    string Name,
    int Capacity,
    decimal BasePricePerHour
);

public record CreateRoomDto(
    string Name,
    int Capacity,
    decimal BasePricePerHour
);

public record UpdateRoomDto(
    string Name,
    int Capacity,
    decimal BasePricePerHour
);

public record ResponseRoomDto(
    int Id,
    string Name,
    int Capacity,
    decimal BasePricePerHour
);
