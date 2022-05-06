using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface ISubscriptionService
{
    Task<IEnumerable<SubscriptionDTO>> GetSubscriptions(int creditCardId);
    Task<SubscriptionDTO> GetById(int? id);
    Task<SubscriptionDTO> GetSubscriptionCreditCard(int? id);
    Task Add(SubscriptionDTO subscriptionDTO);
    Task Update(SubscriptionDTO subscriptionDTO);
    Task Remove(int? id);
}
