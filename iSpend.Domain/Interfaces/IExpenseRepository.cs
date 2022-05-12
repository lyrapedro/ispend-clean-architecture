﻿using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IExpenseRepository
{
    Task<IEnumerable<Expense>> GetExpenses(string userId);
    Task<Expense> GetById(string userId, int? id);
    Task<IEnumerable<Expense>> GetByName(string userId, string name);
    Task<Expense> Create(Expense expense);
    Task<Expense> Update(Expense expense);
    Task<Expense> Remove(Expense expense);
}
