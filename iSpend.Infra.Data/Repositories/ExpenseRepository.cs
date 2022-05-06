using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;
using iSpend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iSpend.Infra.Data.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    ApplicationDbContext _expenseContext;

    public ExpenseRepository(ApplicationDbContext context)
    {
        _expenseContext = context;
    }

    public async Task<Expense> Create(Expense expense)
    {
        _expenseContext.Add(expense);
        await _expenseContext.SaveChangesAsync();
        return expense;
    }

    public async Task<Expense> GetById(int? id)
    {
        return await _expenseContext.Expenses.FindAsync(id);
    }

    public async Task<IEnumerable<Expense>> GetExpenses(string userId)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _expenseContext.Expenses.Where(e => e.UserId == validGuid).ToListAsync();
    }

    public async Task<Expense> Remove(Expense expense)
    {
        _expenseContext.Remove(expense);
        await _expenseContext.SaveChangesAsync();
        return expense;
    }

    public async Task<Expense> Update(Expense expense)
    {
        _expenseContext.Update(expense);
        await _expenseContext.SaveChangesAsync();
        return expense;
    }
}
