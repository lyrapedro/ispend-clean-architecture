using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class GoalService : IGoalService
{
    private IGoalRepository _goalRepository;

    public GoalService(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task<IEnumerable<GoalDto>> GetGoals(string userId)
    {
        var goals = await _goalRepository.GetGoals(userId);
        return goals.Select(g => (GoalDto)g);
    }

    public async Task<GoalDto> GetById(int id)
    {
        var goal = await _goalRepository.GetById(id);
        return (GoalDto)goal;
    }

    public async Task<IEnumerable<GoalDto>> GetByName(string userId, string name)
    {
        IEnumerable<GoalDto> goals;

        if (!string.IsNullOrEmpty(name))
        {
            var query = await _goalRepository.GetByName(userId, name);
            goals = query.Select(g => (GoalDto)g).ToList();
        }
        else
        {
            goals = await GetGoals(userId);
        }

        return goals;
    }

    public async Task Add(GoalDto goalDto)
    {
        var goal = (Goal)goalDto;
        await _goalRepository.Create(goal);
    }

    public async Task Update(GoalDto goalDto)
    {
        var goal = (Goal)goalDto;
        await _goalRepository.Update(goal);
    }

    public async Task Remove(GoalDto goalDto)
    {
        var goal = (Goal)goalDto;
        await _goalRepository.Remove(goal);
    }
}