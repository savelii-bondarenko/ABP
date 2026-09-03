using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

sealed class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    private const short NameMaxLength = 100;
    private const string MoneyType = "decimal(18,2)";

    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder.Property(r => r.Capacity)
            .IsRequired();

        builder.Property(r => r.BasePricePerHour)
            .IsRequired()
            .HasColumnType(MoneyType);

        builder.HasMany(r => r.AvailableServices)
            .WithMany(rs => rs.Rooms)
            .UsingEntity(j => j.ToTable("RoomAvailableServices"));
    }
}
