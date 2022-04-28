using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IExpenseRepository
{
    Task<IEnumerable<Expense>> GetExpensesAsync(string userId);
    Task<Expense> GetExpenseByIdAsync(int? id);
    Task<Expense> CreateAsync(Expense expense);
    Task<Expense> UpdateAsync(Expense expense);
    Task<Expense> RemoveAsync(Expense expense);
}
