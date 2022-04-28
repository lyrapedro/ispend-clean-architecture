﻿using iSpend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iSpend.Infra.Data.EntitiesConfiguration;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).HasMaxLength(25).IsRequired();
        builder.Property(s => s.Price).HasPrecision(10, 2).IsRequired();

        builder.HasOne(s => s.CreditCard).WithMany(c => c.Subscriptions).HasForeignKey(s => s.CreditCardId);
    }
}
