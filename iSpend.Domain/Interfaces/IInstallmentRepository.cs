using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IInstallmentRepository
{
    Task<IEnumerable<Installment>> GetInstallments(string userId);
    Task<Installment> GetById(string userId, int? id);
    Task<IEnumerable<Installment>> GetInstallmentsFromPurchase(string userId, int? purchaseId);
    Task<Purchase> GetInstallmentPurchase(string userId, int? id);
    Task<Installment> Create(Installment installment);
    Task<Installment> Update(Installment installment);
    Task<Installment> Remove(Installment installment);
}
