using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IPurchaseService
{
    Task<IEnumerable<PurchaseDTO>> GetPurchases(string userId);
    Task<IEnumerable<PurchaseDTO>> GetPurchasesFromCreditCard(int creditCardId);
    Task<PurchaseDTO> GetById(int id);
    Task<IEnumerable<PurchaseDTO>> GetByName(string userId, string name);
    Task<CreditCardDTO> GetPurchaseCreditCard(int id);
    Task Add(PurchaseDTO installmentDTO);
    Task Update(PurchaseDTO installmentDTO);
    Task Remove(int id);
}
