using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface ICreditCardRepository
{
    Task<IEnumerable<CreditCard>> GetCreditCardsAsync(string userId);
    Task<CreditCard> GetCreditCardByIdAsync(int? id);
    Task<CreditCard> CreateAsync(CreditCard creditCard);
    Task<CreditCard> UpdateAsync(CreditCard creditCard);
    Task<CreditCard> RemoveAsync(CreditCard creditCard);
}
