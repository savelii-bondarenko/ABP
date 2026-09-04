using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

/// <summary>
/// Represents the database context for the conference room booking application.
/// Manages the entity objects during runtime and coordinates database operations.
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<AdditionalService> AdditionalServices { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

}
