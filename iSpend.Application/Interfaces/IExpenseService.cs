using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IExpenseService
{
    Task<IEnumerable<ExpenseDTO>> GetExpenses(string userId);
    Task<ExpenseDTO> GetById(int id);
    Task<IEnumerable<ExpenseDTO>> GetByName(string userId, string name);
    Task Add(ExpenseDTO expenseDTO);
    Task Update(ExpenseDTO expenseDTO);
    Task Remove(ExpenseDTO expenseDTO);
}
