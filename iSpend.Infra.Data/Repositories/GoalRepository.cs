﻿using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;
using iSpend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iSpend.Infra.Data.Repositories;

public class GoalRepository : IGoalRepository
{
    ApplicationDbContext _goalContext;

    public GoalRepository(ApplicationDbContext context)
    {
        _goalContext = context;
    }

    public async Task<Goal> GetById(string userId, int? id)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _goalContext.Goals.FirstOrDefaultAsync(g => g.UserId == validGuid && g.Id == id);
    }

    public async Task<IEnumerable<Goal>> GetByName(string userId, string name)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _goalContext.Goals.Where(g => g.UserId == validGuid && g.Name.Contains(name)).ToListAsync();
    }

    public async Task<IEnumerable<Goal>> GetGoals(string userId)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _goalContext.Goals.Where(g => g.UserId == validGuid).ToListAsync();
    }

    public async Task<Goal> Create(Goal expense)
    {
        _goalContext.Add(expense);
        await _goalContext.SaveChangesAsync();
        return expense;
    }

    public async Task<Goal> Update(Goal expense)
    {
        _goalContext.Update(expense);
        await _goalContext.SaveChangesAsync();
        return expense;
    }

    public async Task<Goal> Remove(Goal expense)
    {
        _goalContext.Remove(expense);
        await _goalContext.SaveChangesAsync();
        return expense;
    }
}
