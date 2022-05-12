using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface ISubscriptionService
{
    Task<IEnumerable<SubscriptionDTO>> GetSubscriptions(string userId);
    Task<IEnumerable<SubscriptionDTO>> GetSubscriptionsFromCreditCard(string userId, int creditCardId);
    Task<SubscriptionDTO> GetById(string userId, int? id);
    Task<IEnumerable<SubscriptionDTO>> GetByName(string userId, string name);
    Task<CreditCardDTO> GetSubscriptionCreditCard(string userId, int? id);
    Task Add(SubscriptionDTO subscriptionDTO);
    Task Update(SubscriptionDTO subscriptionDTO);
    Task Remove(string userId, int? id);
}
