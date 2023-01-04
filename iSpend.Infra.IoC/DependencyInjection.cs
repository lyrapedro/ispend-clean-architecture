using iSpend.Application.Interfaces;
using iSpend.Application.Services;
using iSpend.Domain.Account;
using iSpend.Domain.Interfaces;
using iSpend.Infra.Data.Context;
using iSpend.Infra.Data.Identity;
using iSpend.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iSpend.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
        ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IAuthenticate, AuthenticateService>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

        services.AddScoped<ICreditCardRepository, CreditCardRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IInstallmentRepository, InstallmentRepository>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        services.AddScoped<ICreditCardService, CreditCardService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IGoalService, GoalService>();
        services.AddScoped<IIncomeService, IncomeService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IInstallmentService, InstallmentService>();
        services.AddScoped<IPurchaseService, PurchaseService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();

        return services;
    }
}
