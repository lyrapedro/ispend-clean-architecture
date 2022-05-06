using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IGoalRepository
{
    Task<IEnumerable<Goal>> GetGoals(string userId);
    Task<Goal> GetGoalById(int? id);
    Task<Goal> Create(Goal goal);
    Task<Goal> Update(Goal goal);
    Task<Goal> Remove(Goal goal);
}
