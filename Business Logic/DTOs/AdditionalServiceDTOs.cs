namespace BusinessLogic.DTOs;

public record AdditionalServiceDto(
    int Id,
    string Name,
    decimal Price
);

public record CreateAdditionalServiceDto(
    string Name,
    decimal Price
);

public record UpdateAdditionalServiceDto(
    int Id,
    string Name,
    decimal Price
);
