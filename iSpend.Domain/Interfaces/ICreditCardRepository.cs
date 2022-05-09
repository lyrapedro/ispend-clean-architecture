using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface ICreditCardRepository
{
    Task<IEnumerable<CreditCard>> GetCreditCardsAsync(string userId);
    Task<CreditCard> GetByIdAsync(int? id);
    Task<CreditCard> CreateAsync(CreditCard creditCard);
    Task<CreditCard> UpdateAsync(CreditCard creditCard);
    Task<CreditCard> RemoveAsync(CreditCard creditCard);
}
