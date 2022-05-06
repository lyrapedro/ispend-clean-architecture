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

    public async Task<Installment> Create(Installment installment)
    {
        _installmentContext.Add(installment);
        await _installmentContext.SaveChangesAsync();
        return installment;
    }

    public async Task<Installment> GetById(int? id)
    {
        return await _installmentContext.Installments.FindAsync(id);
    }

    public async Task<Installment> GetInstallmentPurchase(int? id)
    {
        return await _installmentContext.Installments.Include(i => i.Purchase).SingleOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Installment>> GetInstallments(int purchaseId)
    {
        return await _installmentContext.Installments.Where(i => i.PurchaseId == purchaseId).ToListAsync();
    }

    public async Task<Installment> Remove(Installment installment)
    {
        _installmentContext.Remove(installment);
        await _installmentContext.SaveChangesAsync();
        return installment;
    }

    public async Task<Installment> Update(Installment installment)
    {
        _installmentContext.Update(installment);
        await _installmentContext.SaveChangesAsync();
        return installment;
    }
}
