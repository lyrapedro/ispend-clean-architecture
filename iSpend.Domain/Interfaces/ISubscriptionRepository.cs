using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface ISubscriptionRepository
{
    Task<IEnumerable<Subscription>> GetSubscriptionsAsync(int creditCardId);
    Task<Subscription> GetSubscriptionByIdAsync(int? id);
    Task<Subscription> GetSubscriptionCreditCardAsync(int? id);
    Task<Subscription> CreateAsync(Subscription subscription);
    Task<Subscription> UpdateAsync(Subscription subscription);
    Task<Subscription> RemoveAsync(Subscription subscription);
}
