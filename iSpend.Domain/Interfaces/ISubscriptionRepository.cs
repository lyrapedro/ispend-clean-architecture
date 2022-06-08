using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface ISubscriptionRepository
{
    Task<IEnumerable<Subscription>> GetSubscriptions(string userId);
    Task<IEnumerable<Subscription>> GetSubscriptionsFromCreditCard(int creditCardId);
    Task<Subscription> GetById(int id);
    Task<IEnumerable<Subscription>> GetByName(string userId, string name);
    Task<Subscription> Create(Subscription subscription);
    Task<Subscription> Update(Subscription subscription);
    Task<Subscription> Remove(Subscription subscription);
}
