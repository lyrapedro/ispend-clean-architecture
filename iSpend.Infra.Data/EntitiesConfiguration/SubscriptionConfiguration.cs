using iSpend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iSpend.Infra.Data.EntitiesConfiguration;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(c => c.CreditCardId).IsRequired();
        builder.Property(s => s.Name).HasMaxLength(30).IsRequired();
        builder.Property(s => s.Price).HasPrecision(18, 2).IsRequired();
        builder.Property(s => s.BillingDay).IsRequired();
        builder.Property(s => s.CreditCardId).IsRequired();

        builder.HasOne(s => s.CreditCard).WithMany(c => c.Subscriptions).HasForeignKey(s => s.CreditCardId);
    }
}

public class SubscriptionNodeConfiguration : IEntityTypeConfiguration<SubscriptionNode>
{
    public void Configure(EntityTypeBuilder<SubscriptionNode> builder)
    {
        builder.ToTable("Subscription_Node");
        builder.HasKey(s => s.Id);
        builder.Property(i => i.SubscriptionId).IsRequired();
        builder.Property(i => i.ReferenceDate).IsRequired();

        builder.HasOne(s => s.Subscription).WithMany(c => c.SubscriptionNodes).HasForeignKey(s => s.SubscriptionId);
    }
}
