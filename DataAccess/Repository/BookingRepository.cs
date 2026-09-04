using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

/// <summary>
/// Provides the Entity Framework Core implementation for the booking repository.
/// </summary>
/// <param name="context">The application's database context.</param>
internal class BookingRepository(AppDbContext context) : IBookingRepository
{
    /// <inheritdoc/>
    public async Task<Booking> AddAsync(Booking booking)
    {
        ArgumentNullException.ThrowIfNull(booking);

        await context.Bookings.AddAsync(booking);
        await context.SaveChangesAsync();

        return booking;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id)
    {
        await context.Bookings
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Booking booking)
    {
        ArgumentNullException.ThrowIfNull(booking);

        context.Bookings.Update(booking);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await context.Bookings.AsNoTracking().ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<Booking?> GetByIdAsync(int id)
    {
        return await context.Bookings.FindAsync(id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Booking>> GetOverlappingBookingsAsync(int roomId, DateTime start, DateTime end)
    {
        return await context.Bookings
            .AsNoTracking()
            .Where(b => b.RoomId == roomId && b.StartTime < end && b.EndTime > start)
            .ToListAsync();
    }
}
