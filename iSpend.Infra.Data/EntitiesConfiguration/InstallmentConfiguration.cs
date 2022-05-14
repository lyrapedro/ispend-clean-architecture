using iSpend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iSpend.Infra.Data.EntitiesConfiguration;

public class InstallmentConfiguration : IEntityTypeConfiguration<Installment>
{
    public void Configure(EntityTypeBuilder<Installment> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.PurchaseId).IsRequired();
        builder.Property(i => i.Price).HasPrecision(18, 2).IsRequired();
        builder.Property(i => i.Order).IsRequired();

        builder.HasOne(i => i.Purchase).WithMany(p => p.Installments).HasForeignKey(i => i.PurchaseId);
    }
}
