using iSpend.Application.Interfaces;
using iSpend.Application.Mappings;
using iSpend.Application.Services;
using iSpend.Domain.Interfaces;
using iSpend.Infra.Data.Context;
using iSpend.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iSpend.Infra.IoC;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
        ), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.ConfigureApplicationCookie(options =>
                options.AccessDeniedPath = "api/Account/Login");

        services.AddScoped<ICreditCardRepository, CreditCardRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<IInstallmentRepository, InstallmentRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        services.AddScoped<ICreditCardService, CreditCardService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IGoalService, GoalService>();
        services.AddScoped<IIncomeService, IncomeService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IPurchaseService, PurchaseService>();
        services.AddScoped<IInstallmentService, InstallmentService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        return services;
    }
}
