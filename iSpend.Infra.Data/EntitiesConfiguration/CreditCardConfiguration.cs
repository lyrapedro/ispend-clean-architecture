using iSpend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iSpend.Infra.Data.EntitiesConfiguration;

public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
{
    public void Configure(EntityTypeBuilder<CreditCard> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.Name).HasMaxLength(30).IsRequired();
        builder.Property(c => c.Limit).HasPrecision(18, 2).IsRequired();
        builder.Property(c => c.ClosingDay).IsRequired();
        builder.Property(c => c.ExpirationDay).IsRequired();
    }
}
