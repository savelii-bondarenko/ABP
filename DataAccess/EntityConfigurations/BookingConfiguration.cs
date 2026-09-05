using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

/// <summary>
/// Database scheme conf. for <see cref="Booking"/> entity
/// </summary>
public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    private const string TableName = "BookingSelectedServices";
    private const string MoneyType = "decimal(18,2)";

    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.RoomId)
            .IsRequired();

        builder.Property(b => b.TotalPrice)
               .HasColumnType(MoneyType);

        builder.HasOne(b => b.Room)
            .WithMany(r => r.Bookings)
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.SelectedServices)
               .WithMany(s => s.Bookings)
               .UsingEntity(j => j.ToTable(TableName));
    }
}
