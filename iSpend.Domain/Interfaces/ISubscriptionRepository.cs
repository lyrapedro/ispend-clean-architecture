using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface ISubscriptionRepository
{
    Task<IEnumerable<Subscription>> GetSubscriptionsAsync();
    Task<Subscription> GetSubscriptionByIdAsync(int? id);
    Task<Subscription> CreateAsync(Subscription subscription);
    Task<Subscription> UpdateAsync(Subscription subscription);
    Task<Subscription> RemoveAsync(Subscription subscription);
}
