using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;
using iSpend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iSpend.Infra.Data.Repositories;

public class IncomeRepository : IIncomeRepository
{
    ApplicationDbContext _incomeContext;

    public IncomeRepository(ApplicationDbContext context)
    {
        _incomeContext = context;
    }

    public async Task<Income> Create(Income expense)
    {
        _incomeContext.Add(expense);
        await _incomeContext.SaveChangesAsync();
        return expense;
    }

    public async Task<Income> GetById(string userId, int? id)
    {
        return await _incomeContext.Incomes.FirstOrDefaultAsync(i => i.UserId == userId && i.Id == id);
    }

    public async Task<IEnumerable<Income>> GetByName(string userId, string name)
    {
        return await _incomeContext.Incomes.Where(i => i.UserId == userId && i.Name.Contains(name)).ToListAsync();
    }

    public async Task<IEnumerable<Income>> GetIncomes(string userId)
    {
        return await _incomeContext.Incomes.Where(i => i.UserId == userId).ToListAsync();
    }

    public async Task<Income> Remove(Income expense)
    {
        _incomeContext.Remove(expense);
        await _incomeContext.SaveChangesAsync();
        return expense;
    }

    public async Task<Income> Update(Income expense)
    {
        _incomeContext.Update(expense);
        await _incomeContext.SaveChangesAsync();
        return expense;
    }
}
