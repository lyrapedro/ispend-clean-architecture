using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IExpenseService
{
    Task<IEnumerable<ExpenseDto>> GetExpenses(string userId);
    Task<ExpenseDto> GetById(int id);
    Task<IEnumerable<ExpenseDto>> GetByName(string userId, string name);
    Task Add(ExpenseDto expenseDto);
    Task Update(ExpenseDto expenseDto);
    Task Remove(ExpenseDto expenseDto);
}
