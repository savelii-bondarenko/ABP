namespace DataAccess.Entities;

sealed class Room
{
    public required int Id { get; set;  }

    public required string Name { get; set; }

    public required int Capacity { get; set; }

    public required decimal Rent { get; set; }

    public IList<RoomService> RoomServices { get; set; } = [];

    public IList<Booking> Bookings { get; set; } = [];
}
