using AutoMapper;
using iSpend.Application.DTOs;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class GoalService
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

    public async Task<GoalDTO> GetById(int? id)
    {
        var goal = await _goalRepository.GetGoalById(id);
        return _mapper.Map<GoalDTO>(goal);
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

    public async Task Remove(int? id)
    {
        var goal = _goalRepository.GetGoalById(id).Result;
        await _goalRepository.Remove(goal);
    }
}
