using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IExpenseService
{
    Task<IEnumerable<ExpenseDTO>> GetExpenses(string userId);
    Task<ExpenseDTO> GetById(string userId, int? id);
    Task<IEnumerable<ExpenseDTO>> GetByName(string userId, string name);
    Task Add(ExpenseDTO creditCardDTO);
    Task Update(ExpenseDTO creditCardDTO);
    Task Remove(string userId, int? id);
}
