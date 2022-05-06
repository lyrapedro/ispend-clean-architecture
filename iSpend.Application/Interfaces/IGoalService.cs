using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IGoalService
{
    Task<IEnumerable<GoalDTO>> GetGoals(string userId);
    Task<GoalDTO> GetById(int? id);
    Task Add(GoalDTO creditCardDTO);
    Task Update(GoalDTO creditCardDTO);
    Task Remove(int? id);
}
