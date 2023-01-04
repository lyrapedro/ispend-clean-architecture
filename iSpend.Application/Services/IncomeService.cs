using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class IncomeService : IIncomeService
{
    private IIncomeRepository _incomeRepository;

    public IncomeService(IIncomeRepository incomeRepository)
    {
        _incomeRepository = incomeRepository;
    }

    public async Task<IEnumerable<IncomeDto>> GetIncomes(string userId)
    {
        var incomes = await _incomeRepository.GetIncomes(userId);
        return incomes.Select(i => (IncomeDto)i);
    }

    public async Task<IncomeDto> GetById(int id)
    {
        var income = await _incomeRepository.GetById(id);
        return (IncomeDto)income;
    }

    public async Task<IEnumerable<IncomeDto>> GetByName(string userId, string name)
    {
        IEnumerable<IncomeDto> incomes;

        if (!string.IsNullOrEmpty(name))
        {
            var query = await _incomeRepository.GetByName(userId, name);
            incomes = query.Select(i => (IncomeDto)i);
        }
        else
        {
            incomes = await GetIncomes(userId);
        }

        return incomes;
    }

    public async Task Add(IncomeDto incomeDto)
    {
        var income = (Income)incomeDto;
        await _incomeRepository.Create(income);
    }

    public async Task Update(IncomeDto incomeDto)
    {
        var income = (Income)incomeDto;
        await _incomeRepository.Update(income);
    }

    public async Task Remove(IncomeDto incomeDto)
    {
        var income = (Income)incomeDto;
        await _incomeRepository.Remove(income);
    }
}