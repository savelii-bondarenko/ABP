namespace DataAccess.Entities;

sealed class Room
{
    public int Id { get; set;  }

    public required string Name { get; set; }

    public required int Capacity { get; set; }

    public required decimal BasePricePerHour { get; set; }

    public IList<RoomService> AvailableServices { get; set; } = [];

    public IList<Booking> Bookings { get; set; } = [];
}
