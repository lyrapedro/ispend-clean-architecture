using iSpend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iSpend.Infra.Data.EntitiesConfiguration;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(30).IsRequired();
        builder.Property(e => e.Value).HasPrecision(18, 2).IsRequired();
        builder.Property(e => e.Recurrent).IsRequired();
        builder.Property(e => e.BillingDay).IsRequired();

        builder.HasOne(e => e.Category).WithMany(c => c.Expenses).HasForeignKey(e => e.CategoryId);
    }
}

public class ExpenseNodeConfiguration : IEntityTypeConfiguration<ExpenseNode>
{
    public void Configure(EntityTypeBuilder<ExpenseNode> builder)
    {
        builder.ToTable("Expense_Node");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.ExpenseId).IsRequired();
        builder.Property(e => e.ReferenceDate).IsRequired();

        builder.HasOne(e => e.Expense).WithMany(c => c.ExpenseNodes).HasForeignKey(e => e.ExpenseId);
    }
}

