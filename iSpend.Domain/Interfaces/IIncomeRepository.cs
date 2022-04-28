using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IIncomeRepository
{
    Task<IEnumerable<Income>> GetIncomesAsync(string userId);
    Task<Income> GetIncomeByIdAsync(int? id);
    Task<Income> CreateAsync(Income income);
    Task<Income> UpdateAsync(Income income);
    Task<Income> RemoveAsync(Income income);
}
