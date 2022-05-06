﻿using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;
using iSpend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iSpend.Infra.Data.Repositories;

public class IncomeRepository : IIncomeRepository
{
    ApplicationDbContext _incomeContext;

    public IncomeRepository(ApplicationDbContext context)
    {
        _incomeContext = context;
    }

    public async Task<Income> Create(Income expense)
    {
        _incomeContext.Add(expense);
        await _incomeContext.SaveChangesAsync();
        return expense;
    }

    public async Task<Income> GetById(int? id)
    {
        return await _incomeContext.Incomes.FindAsync(id);
    }

    public async Task<IEnumerable<Income>> GetIncomes(string userId)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _incomeContext.Incomes.Where(i => i.UserId == validGuid).ToListAsync();
    }

    public async Task<Income> Remove(Income expense)
    {
        _incomeContext.Remove(expense);
        await _incomeContext.SaveChangesAsync();
        return expense;
    }

    public async Task<Income> Update(Income expense)
    {
        _incomeContext.Update(expense);
        await _incomeContext.SaveChangesAsync();
        return expense;
    }
}