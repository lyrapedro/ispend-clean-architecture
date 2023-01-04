using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IIncomeService
{
    Task<IEnumerable<IncomeDto>> GetIncomes(string userId);
    Task<IncomeDto> GetById(int id);
    Task<IEnumerable<IncomeDto>> GetByName(string userId, string name);
    Task Add(IncomeDto incomeDTO);
    Task Update(IncomeDto incomeDTO);
    Task Remove(IncomeDto incomeDTO);
}
