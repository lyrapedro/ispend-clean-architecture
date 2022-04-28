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

    public async Task<Installment> CreateAsync(Installment installment)
    {
        _installmentContext.Add(installment);
        await _installmentContext.SaveChangesAsync();
        return installment;
    }

    public async Task<Installment> GetInstallmentByIdAsync(int? id)
    {
        return await _installmentContext.Installments.FindAsync(id);
    }

    public async Task<Installment> GetInstallmentPurchaseAsync(int? id)
    {
        return await _installmentContext.Installments.Include(i => i.Purchase).SingleOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Installment>> GetInstallmentsAsync(int purchaseId)
    {
        return await _installmentContext.Installments.Where(i => i.PurchaseId == purchaseId).ToListAsync();
    }

    public async Task<Installment> RemoveAsync(Installment installment)
    {
        _installmentContext.Remove(installment);
        await _installmentContext.SaveChangesAsync();
        return installment;
    }

    public async Task<Installment> UpdateAsync(Installment installment)
    {
        _installmentContext.Update(installment);
        await _installmentContext.SaveChangesAsync();
        return installment;
    }
}
