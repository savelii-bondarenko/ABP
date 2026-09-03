namespace DataAccess.Entities;

sealed class Booking
{
    public required int Id { get; set; }

    public required int RoomId { get; set; }

    public required DateTime StartTime { get; set; }

    public required DateTime EndTime { get; set; }

    public required float TotalPrice { get; set; }
}
