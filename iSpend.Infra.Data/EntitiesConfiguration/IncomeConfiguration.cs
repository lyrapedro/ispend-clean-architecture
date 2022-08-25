using iSpend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iSpend.Infra.Data.EntitiesConfiguration;

public class IncomeConfiguration : IEntityTypeConfiguration<Income>
{
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(i => i.Name).HasMaxLength(30).IsRequired();
        builder.Property(i => i.Value).HasPrecision(18, 2).IsRequired();
        builder.Property(i => i.Recurrent).IsRequired();
        builder.Property(i => i.Payday).IsRequired();

        builder.HasOne(i => i.Category).WithMany(c => c.Incomes).HasForeignKey(p => p.CategoryId);
    }
}

public class IncomeNodeConfiguration : IEntityTypeConfiguration<IncomeNode>
{
    public void Configure(EntityTypeBuilder<IncomeNode> builder)
    {
        builder.ToTable("Income_Node");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.IncomeId).IsRequired();
        builder.Property(i => i.ReferenceDate).IsRequired();

        builder.HasOne(i => i.Income).WithMany(c => c.IncomeNodes).HasForeignKey(p => p.IncomeId);
    }
}
