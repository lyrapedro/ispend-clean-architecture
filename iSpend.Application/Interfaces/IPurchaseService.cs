using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IPurchaseService
{
    Task<IEnumerable<PurchaseDTO>> GetPurchases(string userId);
    Task<IEnumerable<PurchaseDTO>> GetPurchasesFromCreditCard(string userId, int creditCardId);
    Task<PurchaseDTO> GetById(string userId, int? id);
    Task<IEnumerable<PurchaseDTO>> GetByName(string userId, string name);
    Task<CreditCardDTO> GetPurchaseCreditCard(int id);
    Task Add(PurchaseDTO installmentDTO);
    Task Update(PurchaseDTO installmentDTO);
    Task Remove(string userId, int? id);
}
