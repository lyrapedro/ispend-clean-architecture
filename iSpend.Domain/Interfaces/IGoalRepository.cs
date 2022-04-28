using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IGoalRepository
{
    Task<IEnumerable<Goal>> GetGoalsAsync();
    Task<Goal> GetGoalByIdAsync(int? id);
    Task<Goal> CreateAsync(Goal goal);
    Task<Goal> UpdateAsync(Goal goal);
    Task<Goal> RemoveAsync(Goal goal);
}
