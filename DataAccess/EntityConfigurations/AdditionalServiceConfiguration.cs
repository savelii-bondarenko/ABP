using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

/// <summary>
/// Database scheme conf. for <see cref="AdditionalService"/> entity
/// </summary>
internal sealed class AdditionalServiceConfiguration : IEntityTypeConfiguration<AdditionalService>
{
    private const string MoneyType = "decimal(18,2)";

    public void Configure(EntityTypeBuilder<AdditionalService> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);

        builder.Property(s => s.Price)
               .HasColumnType(MoneyType);
    }
}
