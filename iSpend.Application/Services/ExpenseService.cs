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

        var expensesDto = _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);

        foreach (var dto in expensesDto)
        {
            dto.Late = await HasPendingPayment(dto.Id, dto.BillingDay);
        }

        return expensesDto;
    }

    public async Task<ExpenseDTO> GetById(int id)
    {
        var expense = await _expenseRepository.GetById(id);
        bool hasPendingPayment = await HasPendingPayment(id, expense.BillingDay);

        var expenseDto = _mapper.Map<ExpenseDTO>(expense);
        expenseDto.Late = hasPendingPayment;

        return expenseDto;
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

        foreach (var expense in expenses)
        {
            expense.Late = await HasPendingPayment(expense.Id, expense.BillingDay);
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

    public async Task Remove(ExpenseDTO expenseDTO)
    {
        var expense = _mapper.Map<Expense>(expenseDTO);
        await _expenseRepository.Remove(expense);
    }

    #region Util
    public async Task<bool> HasPendingPayment(int expenseId, int billingDay)
    {
        var todayDate = DateTime.Now;
        var paymentDate = new DateTime(todayDate.Year, todayDate.Month, billingDay);
        var alreadyPaid = await _expenseRepository.GetAlreadyPaid(expenseId);

        var lastPayment = alreadyPaid.OrderBy(x => x.ReferenceDate).FirstOrDefault();

        if (lastPayment != null)
        {
            if (lastPayment.ReferenceDate.Month == todayDate.Month && lastPayment.ReferenceDate.Year == todayDate.Year)
                return false;
        }

        if (todayDate.Date <= paymentDate.Date)
            return false;

        return true;
    }
    #endregion
}
