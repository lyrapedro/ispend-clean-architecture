using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface ISubscriptionService
{
    Task<IEnumerable<SubscriptionDto>> GetSubscriptions(string userId);
    Task<IEnumerable<SubscriptionDto>> GetSubscriptionsFromCreditCard(int creditCardId);
    Task<SubscriptionDto> GetById(int id);
    Task<IEnumerable<SubscriptionDto>> GetByName(string userId, string name);
    Task Add(SubscriptionDto subscriptionDTO);
    Task Update(SubscriptionDto subscriptionDTO);
    Task Remove(SubscriptionDto subscriptionDTO);
}
