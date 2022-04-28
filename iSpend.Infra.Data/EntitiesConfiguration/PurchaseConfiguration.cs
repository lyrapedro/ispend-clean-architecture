using iSpend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iSpend.Infra.Data.EntitiesConfiguration;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(25).IsRequired();
        builder.Property(p => p.Price).HasPrecision(10, 2).IsRequired();

        builder.HasOne(p => p.CreditCard).WithMany(c => c.Purchases).HasForeignKey(p => p.CreditCardId);
    }
}
