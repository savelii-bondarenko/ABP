namespace DataAccess.Entities;

sealed class RoomService
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required decimal Price { get; set; }

    public IList<Room> Rooms { get; set; } = [];

    public IList<Booking> Bookings { get; set; } = [];
}
