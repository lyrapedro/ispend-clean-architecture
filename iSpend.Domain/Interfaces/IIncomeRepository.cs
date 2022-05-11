using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IIncomeRepository
{
    Task<IEnumerable<Income>> GetIncomes(string userId);
    Task<Income> GetById(string userId, int? id);
    Task<IEnumerable<Income>> GetByName(string userId, string name);
    Task<Income> Create(Income income);
    Task<Income> Update(Income income);
    Task<Income> Remove(Income income);
}
