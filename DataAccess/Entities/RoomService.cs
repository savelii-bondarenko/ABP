namespace DataAccess.Entities;

sealed class RoomService
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required float Price { get; set; }
}
