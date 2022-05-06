using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IInstallmentRepository
{
    Task<IEnumerable<Installment>> GetInstallments(int purchaseId);
    Task<Installment> GetById(int? id);
    Task<Installment> GetInstallmentPurchase(int? id);
    Task<Installment> Create(Installment installment);
    Task<Installment> Update(Installment installment);
    Task<Installment> Remove(Installment installment);
}
