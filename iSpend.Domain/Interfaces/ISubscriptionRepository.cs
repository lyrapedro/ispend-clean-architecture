using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface ISubscriptionRepository
{
    Task<IEnumerable<Subscription>> GetSubscriptions(string userId);
    Task<IEnumerable<Subscription>> GetSubscriptionsFromCreditCard(string userId, int creditCardId);
    Task<Subscription> GetById(string userId, int? id);
    Task<IEnumerable<Subscription>> GetByName(string userId, string name);
    Task<CreditCard> GetSubscriptionCreditCard(string userId, int? id);
    Task<Subscription> Create(Subscription subscription);
    Task<Subscription> Update(Subscription subscription);
    Task<Subscription> Remove(Subscription subscription);
}
