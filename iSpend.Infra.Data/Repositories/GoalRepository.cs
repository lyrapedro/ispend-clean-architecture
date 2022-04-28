using iSpend.Domain.Entities;
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

    public async Task<Goal> CreateAsync(Goal expense)
    {
        _goalContext.Add(expense);
        await _goalContext.SaveChangesAsync();
        return expense;
    }

    public async Task<Goal> GetGoalByIdAsync(int? id)
    {
        return await _goalContext.Goals.FindAsync(id);
    }

    public async Task<IEnumerable<Goal>> GetGoalsAsync(string userId)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _goalContext.Goals.Where(g => g.UserId == validGuid).ToListAsync();
    }

    public async Task<Goal> RemoveAsync(Goal expense)
    {
        _goalContext.Remove(expense);
        await _goalContext.SaveChangesAsync();
        return expense;
    }

    public async Task<Goal> UpdateAsync(Goal expense)
    {
        _goalContext.Update(expense);
        await _goalContext.SaveChangesAsync();
        return expense;
    }
}
