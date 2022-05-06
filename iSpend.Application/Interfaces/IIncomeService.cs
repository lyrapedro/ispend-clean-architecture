using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IIncomeService
{
    Task<IEnumerable<IncomeDTO>> GetIncomes(string userId);
    Task<IncomeDTO> GetById(int? id);
    Task Add(IncomeDTO creditCardDTO);
    Task Update(IncomeDTO creditCardDTO);
    Task Remove(int? id);
}
