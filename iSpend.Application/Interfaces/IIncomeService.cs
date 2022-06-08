using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IIncomeService
{
    Task<IEnumerable<IncomeDTO>> GetIncomes(string userId);
    Task<IncomeDTO> GetById(int id);
    Task<IEnumerable<IncomeDTO>> GetByName(string userId, string name);
    Task Add(IncomeDTO incomeDTO);
    Task Update(IncomeDTO incomeDTO);
    Task Remove(IncomeDTO incomeDTO);
}
