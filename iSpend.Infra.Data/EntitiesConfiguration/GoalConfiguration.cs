using iSpend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iSpend.Infra.Data.EntitiesConfiguration;

public class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Name).HasMaxLength(25).IsRequired();
        builder.Property(g => g.GoalValue).HasPrecision(10, 2).IsRequired();
        builder.Property(g => g.ValueSaved).HasPrecision(10, 2).IsRequired();
    }
}
