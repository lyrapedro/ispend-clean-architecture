using iSpend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iSpend.Infra.Data.EntitiesConfiguration;

public class IncomeConfiguration : IEntityTypeConfiguration<Income>
{
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Name).HasMaxLength(25).IsRequired();
        builder.Property(i => i.Value).HasPrecision(10, 2).IsRequired();
    }
}
