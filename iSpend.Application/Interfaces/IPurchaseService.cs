using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface IPurchaseService
{
    Task<IEnumerable<PurchaseDTO>> GetPurchases(int creditCardId);
    Task<PurchaseDTO> GetById(int? id);
    Task<PurchaseDTO> GetPurchaseCreditCard(int? id);
    Task Add(PurchaseDTO installmentDTO);
    Task Update(PurchaseDTO installmentDTO);
    Task Remove(int? id);
}
