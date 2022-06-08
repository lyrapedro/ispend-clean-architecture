using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface ISubscriptionService
{
    Task<IEnumerable<SubscriptionDTO>> GetSubscriptions(string userId);
    Task<IEnumerable<SubscriptionDTO>> GetSubscriptionsFromCreditCard(int creditCardId);
    Task<SubscriptionDTO> GetById(int id);
    Task<IEnumerable<SubscriptionDTO>> GetByName(string userId, string name);
    Task Add(SubscriptionDTO subscriptionDTO);
    Task Update(SubscriptionDTO subscriptionDTO);
    Task Remove(SubscriptionDTO subscriptionDTO);
}
