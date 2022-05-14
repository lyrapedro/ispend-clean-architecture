using iSpend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iSpend.Infra.Data.EntitiesConfiguration;

public class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(g => g.StartDate).IsRequired();
        builder.Property(g => g.EndDate).IsRequired();
        builder.Property(g => g.Name).HasMaxLength(30).IsRequired();
        builder.Property(g => g.Description).HasMaxLength(200);
        builder.Property(g => g.GoalValue).HasPrecision(18, 2).IsRequired();
        builder.Property(g => g.ValueSaved).HasPrecision(18, 2);
    }
}
