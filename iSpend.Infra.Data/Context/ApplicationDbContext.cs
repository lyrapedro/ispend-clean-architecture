using iSpend.Domain.Entities;
using iSpend.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace iSpend.Infra.Data.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<CreditCard> CreditCards { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseNode> ExpenseNodes { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<IncomeNode> IncomeNodes { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Installment> Installments { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<SubscriptionNode> SubscriptionNodes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");

        builder.Properties<DateOnly?>()
            .HaveConversion<NullableDateOnlyConverter>()
            .HaveColumnType("date");
    }
}

/// <summary>
/// Converts <see cref="DateOnly" /> to <see cref="DateTime"/> and vice versa.
/// </summary>
public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(
            d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d))
    { }
}

/// <summary>
/// Converts <see cref="DateOnly?" /> to <see cref="DateTime?"/> and vice versa.
/// </summary>
public class NullableDateOnlyConverter : ValueConverter<DateOnly?, DateTime?>
{
    public NullableDateOnlyConverter() : base(
        d => d == null
            ? null
            : new DateTime?(d.Value.ToDateTime(TimeOnly.MinValue)),
        d => d == null
            ? null
            : new DateOnly?(DateOnly.FromDateTime(d.Value)))
    { }
}
