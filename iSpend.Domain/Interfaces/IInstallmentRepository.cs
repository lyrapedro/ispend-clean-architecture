using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface IInstallmentRepository
{
    Task<IEnumerable<Installment>> GetInstallments(string userId);
    Task<Installment> GetById(int id);
    Task<IEnumerable<Installment>> GetInstallmentsFromPurchase(string userId, int purchaseId);
    Task<Installment> Create(Installment installment);
    Task<List<Installment>> CreateInstallments(List<Installment> installment);
    Task<Installment> Update(Installment installment);
    Task<Installment> Remove(Installment installment);
}
