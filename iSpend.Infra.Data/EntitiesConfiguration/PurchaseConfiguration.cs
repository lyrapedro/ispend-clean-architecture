using iSpend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iSpend.Infra.Data.EntitiesConfiguration;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(c => c.CreditCardId).IsRequired();
        builder.Property(p => p.Name).HasMaxLength(30).IsRequired();
        builder.Property(p => p.Price).HasPrecision(18, 2).IsRequired();
        builder.Property(p => p.PurchasedAt).IsRequired();

        builder.HasOne(p => p.CreditCard).WithMany(c => c.Purchases).HasForeignKey(p => p.CreditCardId);
        builder.HasOne(p => p.Category).WithMany(c => c.Purchases).HasForeignKey(p => p.CategoryId);
    }
}
