using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseService(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<IEnumerable<ExpenseDto>> GetExpenses(string userId)
    {
        var expenses = await _expenseRepository.GetExpenses(userId);

        var expensesDto = expenses.Select(e => (ExpenseDto)e);

        var expenseDtos = expensesDto.ToList();
        foreach (var dto in expenseDtos)
        {
            dto.Late = await HasLatePayment(dto.Id, dto.BillingDay);
        }

        return expenseDtos;
    }

    public async Task<ExpenseDto> GetById(int id)
    {
        var expense = await _expenseRepository.GetById(id);
        var hasPendingPayment = await HasLatePayment(id, expense.BillingDay);

        var expenseDto = (ExpenseDto)expense;
        expenseDto.Late = hasPendingPayment;

        return expenseDto;
    }

    public async Task<IEnumerable<ExpenseDto>> GetByName(string userId, string name)
    {
        IEnumerable<ExpenseDto> expenses;

        if (!string.IsNullOrEmpty(name))
        {
            var query = await _expenseRepository.GetByName(userId, name);
            expenses = query.Select(c => (ExpenseDto)c);
        }
        else
        {
            expenses = await GetExpenses(userId);
        }

        var expenseDtos = expenses.ToList();
        foreach (var expense in expenseDtos)
        {
            expense.Late = await HasLatePayment(expense.Id, expense.BillingDay);
        }

        return expenseDtos;
    }

    public async Task Add(ExpenseDto expenseDto)
    {
        var expense = (Expense)expenseDto;
        await _expenseRepository.Create(expense);
    }

    public async Task Update(ExpenseDto expenseDto)
    {
        expenseDto.ModifiedAt = DateTime.Now;
        var expense = (Expense)expenseDto;
        await _expenseRepository.Update(expense);
    }

    public async Task Remove(ExpenseDto expenseDto)
    {
        var expense = (Expense)expenseDto;
        await _expenseRepository.Remove(expense);
    }

    #region Util

    private async Task<bool> HasLatePayment(int expenseId, int billingDay)
    {
        var todayDate = DateTime.Now;
        var paymentDate = new DateTime(todayDate.Year, todayDate.Month, billingDay);

        if (todayDate.Date <= paymentDate.Date)
            return false;

        var alreadyPaid = await _expenseRepository.GetAlreadyPaid(expenseId);
        var lastPayment = alreadyPaid.MinBy(x => x.ReferenceDate);

        if (lastPayment == null) return true;
        var lastPaymentWasThisMonth = (lastPayment.ReferenceDate.Month == todayDate.Month &&
                                       lastPayment.ReferenceDate.Year == todayDate.Year);
        if (lastPaymentWasThisMonth)
            return false;

        return true;
    }

    #endregion
}