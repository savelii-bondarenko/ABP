using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

/// <summary>
/// Database scheme conf. for <see cref="RoomService"/> entity
/// </summary>
internal sealed class RoomServiceConfiguration : IEntityTypeConfiguration<RoomService>
{
    private const string MoneyType = "decimal(18,2)";

    public void Configure(EntityTypeBuilder<RoomService> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);

        builder.Property(s => s.Price)
               .HasColumnType(MoneyType);
    }
}
