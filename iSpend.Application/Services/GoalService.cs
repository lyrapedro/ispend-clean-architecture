using AutoMapper;
using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class GoalService : IGoalService
{
    private IGoalRepository _goalRepository;
    private readonly IMapper _mapper;

    public GoalService(IGoalRepository goalRepository, IMapper mapper)
    {
        _goalRepository = goalRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GoalDTO>> GetGoals(string userId)
    {
        var goals = await _goalRepository.GetGoals(userId);
        return _mapper.Map<IEnumerable<GoalDTO>>(goals);
    }

    public async Task<GoalDTO> GetById(string userId, int? id)
    {
        var goal = await _goalRepository.GetById(userId, id);
        return _mapper.Map<GoalDTO>(goal);
    }

    public async Task<IEnumerable<GoalDTO>> GetByName(string userId, string name)
    {
        IEnumerable<GoalDTO> goals;

        if (!string.IsNullOrEmpty(name))
        {
            var query = await _goalRepository.GetByName(userId, name);
            goals = query.Select(c => _mapper.Map<GoalDTO>(c)).ToList();
        }
        else
        {
            goals = await GetGoals(userId);
        }

        return goals;
    }

    public async Task Add(GoalDTO goalDTO)
    {
        var goal = _mapper.Map<Goal>(goalDTO);
        await _goalRepository.Create(goal);
    }

    public async Task Update(GoalDTO goalDTO)
    {
        var goal = _mapper.Map<Goal>(goalDTO);
        await _goalRepository.Update(goal);
    }

    public async Task Remove(string userId, int? id)
    {
        var goal = _goalRepository.GetById(userId, id).Result;
        await _goalRepository.Remove(goal);
    }
}
