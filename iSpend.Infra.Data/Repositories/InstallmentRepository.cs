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

    public async Task<Installment> GetById(string userId, int? id)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _installmentContext.Installments.Include(i => i.Purchase.CreditCard).Where(i => i.Purchase.CreditCard.UserId == validGuid).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Installment>> GetInstallments(string userId)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _installmentContext.Installments.Include(i => i.Purchase.CreditCard).Where(i => i.Purchase.CreditCard.UserId == validGuid).ToListAsync();
    }

    public async Task<IEnumerable<Installment>> GetInstallmentsFromPurchase(string userId, int? purchaseId)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _installmentContext.Installments.Include(i => i.Purchase.CreditCard).Where(i => i.Purchase.CreditCard.UserId == validGuid && i.Purchase.Id == purchaseId).ToListAsync();
    }

    public async Task<Purchase> GetInstallmentPurchase(string userId, int? id)
    {
        Guid validGuid = Guid.Parse(userId);

        var installment = await _installmentContext.Installments.Include(i => i.Purchase.CreditCard).FirstOrDefaultAsync(i => i.Purchase.CreditCard.UserId == validGuid && i.Id == id);
        return installment.Purchase;
    }

    public async Task<Installment> Create(Installment installment)
    {
        _installmentContext.Add(installment);
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
