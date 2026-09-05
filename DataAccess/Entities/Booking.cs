namespace DataAccess.Entities;

public sealed class Booking
{
    public int Id { get; set; }

    public required int RoomId { get; set; }

    public required DateTime StartTime { get; set; }

    public required DateTime EndTime { get; set; }

    public required decimal TotalPrice { get; set; }

    public Room Room { get; set; } = null!;

    public IList<AdditionalService> SelectedServices { get; set; } = [];
}
