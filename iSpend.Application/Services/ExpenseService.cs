using AutoMapper;
using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class ExpenseService : IExpenseService
{
    private IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ExpenseDTO>> GetExpenses(string userId)
    {
        var expenses = await _expenseRepository.GetExpenses(userId);
        return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
    }

    public async Task<ExpenseDTO> GetById(string userId, int? id)
    {
        var expense = await _expenseRepository.GetById(userId, id);
        return _mapper.Map<ExpenseDTO>(expense);
    }

    public async Task<IEnumerable<ExpenseDTO>> GetByName(string userId, string name)
    {
        IEnumerable<ExpenseDTO> expenses;

        if (!string.IsNullOrEmpty(name))
        {
            var query = await _expenseRepository.GetByName(userId, name);
            expenses = query.Select(c => _mapper.Map<ExpenseDTO>(c)).ToList();
        }
        else
        {
            expenses = await GetExpenses(userId);
        }

        return expenses;
    }

    public async Task Add(ExpenseDTO expenseDTO)
    {
        var expense = _mapper.Map<Expense>(expenseDTO);
        await _expenseRepository.Create(expense);
    }

    public async Task Update(ExpenseDTO expenseDTO)
    {
        var expense = _mapper.Map<Expense>(expenseDTO);
        await _expenseRepository.Update(expense);
    }

    public async Task Remove(string userId, int? id)
    {
        var expense = _expenseRepository.GetById(userId, id).Result;
        await _expenseRepository.Remove(expense);
    }
}
