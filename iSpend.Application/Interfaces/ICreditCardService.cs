using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface ICreditCardService
{
    Task<IEnumerable<CreditCardDto>> GetCreditCards(string userId);
    Task<CreditCardDto> GetById(int id);
    Task<IEnumerable<CreditCardDto>> GetByName(string userId, string name);
    Task<CreditCardDto> GetCreditCardFromPurchase(int purchaseId);
    Task<CreditCardDto> GetCreditCardFromSubscription(int subscriptionId);
    Task Add(CreditCardDto CreditCardDto);
    Task Update(CreditCardDto CreditCardDto);
    Task Remove(CreditCardDto CreditCardDto);
}
