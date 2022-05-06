using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface ISubscriptionRepository
{
    Task<IEnumerable<Subscription>> GetSubscriptions(int creditCardId);
    Task<Subscription> GetById(int? id);
    Task<Subscription> GetSubscriptionCreditCard(int? id);
    Task<Subscription> Create(Subscription subscription);
    Task<Subscription> Update(Subscription subscription);
    Task<Subscription> Remove(Subscription subscription);
}
