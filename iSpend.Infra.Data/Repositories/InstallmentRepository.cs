using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;
using iSpend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iSpend.Infra.Data.Repositories;

public class InstallmentRepository : IInstallmentRepository
{
    ApplicationDbContext _installmentContext;

    public InstallmentRepository(ApplicationDbContext context)
    {
        _installmentContext = context;
    }

    public async Task<Installment> GetById(int id)
    {
        return await _installmentContext.Installments.Include(i => i.Purchase.CreditCard).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Installment>> GetInstallments(string userId)
    {
        return await _installmentContext.Installments.Include(i => i.Purchase.CreditCard).Where(i => i.Purchase.CreditCard.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Installment>> GetInstallmentsFromPurchase(string userId, int purchaseId)
    {
        return await _installmentContext.Installments.Include(i => i.Purchase.CreditCard).Where(i => i.Purchase.CreditCard.UserId == userId && i.Purchase.Id == purchaseId).ToListAsync();
    }

    public async Task<Installment> Create(Installment installment)
    {
        _installmentContext.Add(installment);
        await _installmentContext.SaveChangesAsync();
        return installment;
    }

    public async Task<List<Installment>> CreateInstallments(List<Installment> installment)
    {
        _installmentContext.AddRange(installment);
        await _installmentContext.SaveChangesAsync();
        return installment;
    }

    public async Task<Installment> Update(Installment installment)
    {
        _installmentContext.Update(installment);
        await _installmentContext.SaveChangesAsync();
        return installment;
    }

    public async Task<Installment> Remove(Installment installment)
    {
        _installmentContext.Remove(installment);
        await _installmentContext.SaveChangesAsync();
        return installment;
    }
}
