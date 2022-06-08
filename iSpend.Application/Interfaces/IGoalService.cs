﻿using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IGoalService
{
    Task<IEnumerable<GoalDTO>> GetGoals(string userId);
    Task<GoalDTO> GetById(int id);
    Task<IEnumerable<GoalDTO>> GetByName(string userid, string name);
    Task Add(GoalDTO goalDTO);
    Task Update(GoalDTO goalDTO);
    Task Remove(GoalDTO goalDTO);
}
