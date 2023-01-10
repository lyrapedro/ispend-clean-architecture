using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IGoalService
{
    Task<IEnumerable<GoalDto>> GetGoals(string userId);
    Task<GoalDto> GetById(int id);
    Task<IEnumerable<GoalDto>> GetByName(string userid, string name);
    Task Add(GoalDto goalDto);
    Task Update(GoalDto goalDto);
    Task Remove(GoalDto goalDto);
}
