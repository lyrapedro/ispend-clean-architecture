using AutoMapper;
using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class IncomeService : IIncomeService
{
    private IIncomeRepository _incomeRepository;
    private readonly IMapper _mapper;

    public IncomeService(IIncomeRepository incomeRepository, IMapper mapper)
    {
        _incomeRepository = incomeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<IncomeDTO>> GetIncomes(string userId)
    {
        var incomes = await _incomeRepository.GetIncomes(userId);
        return _mapper.Map<IEnumerable<IncomeDTO>>(incomes);
    }

    public async Task<IncomeDTO> GetById(int? id)
    {
        var income = await _incomeRepository.GetById(id);
        return _mapper.Map<IncomeDTO>(income);
    }

    public async Task Add(IncomeDTO incomeDTO)
    {
        var income = _mapper.Map<Income>(incomeDTO);
        await _incomeRepository.Create(income);
    }

    public async Task Update(IncomeDTO incomeDTO)
    {
        var income = _mapper.Map<Income>(incomeDTO);
        await _incomeRepository.Update(income);
    }

    public async Task Remove(int? id)
    {
        var income = _incomeRepository.GetById(id).Result;
        await _incomeRepository.Remove(income);
    }
}
