using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IPurchaseService
{
    Task<IEnumerable<PurchaseDto>> GetPurchases(string userId);
    Task<IEnumerable<PurchaseDto>> GetPurchasesFromCreditCard(int creditCardId);
    Task<PurchaseDto> GetById(int id);
    Task<IEnumerable<PurchaseDto>> GetByName(string userId, string name);
    Task Add(PurchaseDto purchaseDTO);
    Task Update(PurchaseDto purchaseDTO);
    Task Remove(PurchaseDto purchaseDTO);
}
