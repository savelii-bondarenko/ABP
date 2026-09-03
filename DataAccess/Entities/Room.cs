namespace DataAccess.Entities;

sealed class Room
{
    public required int Id { get; set;  }

    public required string Name { get; set; }

    public required int Capacity { get; set; }

    public required float Rent { get; set; }

    public List<RoomService> RoomServices = [];
}
