using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface ICreditCardRepository
{
    Task<IEnumerable<CreditCard>> GetCreditCards(string userId);
    Task<CreditCard> GetById(int id);
    Task<IEnumerable<CreditCard>> GetByName(string userId, string name);
    Task<CreditCard> Create(CreditCard creditCard);
    Task<CreditCard> Update(CreditCard creditCard);
    Task<CreditCard> Remove(CreditCard creditCard);
}
