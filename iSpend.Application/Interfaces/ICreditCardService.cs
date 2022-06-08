using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface ICreditCardService
{
    Task<IEnumerable<CreditCardDTO>> GetCreditCards(string userId);
    Task<CreditCardDTO> GetById(int id);
    Task<IEnumerable<CreditCardDTO>> GetByName(string userId, string name);
    Task<CreditCardDTO> GetCreditCardFromPurchase(int purchaseId);
    Task<CreditCardDTO> GetCreditCardFromSubscription(int subscriptionId);
    Task Add(CreditCardDTO creditCardDTO);
    Task Update(CreditCardDTO creditCardDTO);
    Task Remove(CreditCardDTO creditCardDTO);
}
