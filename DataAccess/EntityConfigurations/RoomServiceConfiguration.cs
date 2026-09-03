using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

sealed class RoomServiceConfiguration : IEntityTypeConfiguration<RoomService>
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
