using iSpend.Domain.Entities;
using iSpend.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iSpend.Infra.Data.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<CreditCard> CreditCards { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Installment> Installments { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(builder);

        builder.Entity<CreditCard>()
            .Property(p => p.Name).IsRequired();
        builder.Entity<CreditCard>()
            .Property(p => p.Limit).IsRequired();
        builder.Entity<CreditCard>()
            .Property(p => p.ClosingDay).IsRequired();
        builder.Entity<CreditCard>()
            .Property(p => p.ExpirationDay).IsRequired();
        builder.Entity<CreditCard>()
            .Property(p => p.UserId).IsRequired();

        builder.Entity<Goal>()
            .Property(c => c.Name).IsRequired();
        builder.Entity<Goal>()
            .Property(c => c.StartDate).IsRequired();
        builder.Entity<Goal>()
            .Property(c => c.EndDate).IsRequired();
        builder.Entity<Goal>()
            .Property(c => c.GoalValue).IsRequired();
        builder.Entity<Goal>()
            .Property(c => c.UserId).IsRequired();

        builder.Entity<Expense>()
            .Property(c => c.Name).IsRequired();
        builder.Entity<Expense>()
            .Property(c => c.Value).IsRequired();
        builder.Entity<Expense>()
            .Property(c => c.BillingDay).IsRequired();
        builder.Entity<Expense>()
            .Property(c => c.Recurrent).IsRequired();
        builder.Entity<Expense>()
            .Property(c => c.Active).IsRequired();
        builder.Entity<Expense>()
            .Property(c => c.UserId).IsRequired();

        builder.Entity<Income>()
            .Property(c => c.Name).IsRequired();
        builder.Entity<Income>()
            .Property(c => c.Value).IsRequired();
        builder.Entity<Income>()
            .Property(c => c.Recurrent).IsRequired();
        builder.Entity<Income>()
            .Property(c => c.Active).IsRequired();
        builder.Entity<Income>()
            .Property(c => c.Payday).IsRequired();
        builder.Entity<Income>()
            .Property(c => c.UserId).IsRequired();

        builder.Entity<Purchase>()
            .Property(c => c.Name).IsRequired();
        builder.Entity<Purchase>()
            .Property(c => c.Price).IsRequired();
        builder.Entity<Purchase>()
            .Property(c => c.FirstInstallmentDate).IsRequired();
        builder.Entity<Purchase>()
            .Property(c => c.CreditCardId).IsRequired();

        builder.Entity<Installment>()
            .Property(c => c.Sequence).IsRequired();
        builder.Entity<Installment>()
            .Property(c => c.Price).IsRequired();
        builder.Entity<Installment>()
            .Property(c => c.Paid).IsRequired();
        builder.Entity<Installment>()
            .Property(c => c.PurchaseId).IsRequired();

        builder.Entity<Subscription>()
            .Property(c => c.Name).IsRequired();
        builder.Entity<Subscription>()
            .Property(c => c.Price).IsRequired();
        builder.Entity<Subscription>()
            .Property(c => c.Active).IsRequired();
        builder.Entity<Subscription>()
            .Property(c => c.CreditCardId).IsRequired();
    }

}
