using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IExpenseService
{
    Task<IEnumerable<ExpenseDTO>> GetExpenses(string userId);
    Task<ExpenseDTO> GetById(int? id);
    Task Add(ExpenseDTO creditCardDTO);
    Task Update(ExpenseDTO creditCardDTO);
    Task Remove(int? id);
}
