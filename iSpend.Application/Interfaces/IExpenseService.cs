using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IExpenseService
{
    Task<IEnumerable<ExpenseDto>> GetExpenses(string userId);
    Task<ExpenseDto> GetById(int id);
    Task<IEnumerable<ExpenseDto>> GetByName(string userId, string name);
    Task Add(ExpenseDto ExpenseDto);
    Task Update(ExpenseDto ExpenseDto);
    Task Remove(ExpenseDto ExpenseDto);
}
