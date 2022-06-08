using AutoMapper;
using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private ISubscriptionRepository _subscriptionRepository;
    private readonly IMapper _mapper;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mapper)
    {
        _subscriptionRepository = subscriptionRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SubscriptionDTO>> GetSubscriptions(string userId)
    {
        var subscriptions = await _subscriptionRepository.GetSubscriptions(userId);
        return _mapper.Map<IEnumerable<SubscriptionDTO>>(subscriptions);
    }

    public async Task<IEnumerable<SubscriptionDTO>> GetSubscriptionsFromCreditCard(int creditCardId)
    {
        var subscription = await _subscriptionRepository.GetSubscriptionsFromCreditCard(creditCardId);
        return _mapper.Map<IEnumerable<SubscriptionDTO>>(subscription);
    }

    public async Task<SubscriptionDTO> GetById(int id)
    {
        var subscription = await _subscriptionRepository.GetById(id);
        return _mapper.Map<SubscriptionDTO>(subscription);
    }

    public async Task<IEnumerable<SubscriptionDTO>> GetByName(string userId, string name)
    {
        IEnumerable<SubscriptionDTO> subscriptions;

        if (!string.IsNullOrEmpty(name))
        {
            var query = await _subscriptionRepository.GetByName(userId, name);
            subscriptions = query.Select(c => _mapper.Map<SubscriptionDTO>(c)).ToList();
        }
        else
        {
            subscriptions = await GetSubscriptions(userId);
        }

        return subscriptions;
    }

    public async Task<CreditCardDTO> GetSubscriptionCreditCard(int id)
    {
        var subscription = await _subscriptionRepository.GetSubscriptionCreditCard(id);
        return _mapper.Map<CreditCardDTO>(subscription);
    }

    public async Task Add(SubscriptionDTO subscriptionDTO)
    {
        var subscription = _mapper.Map<Subscription>(subscriptionDTO);
        await _subscriptionRepository.Create(subscription);
    }

    public async Task Update(SubscriptionDTO subscriptionDTO)
    {
        var subscription = _mapper.Map<Subscription>(subscriptionDTO);
        await _subscriptionRepository.Update(subscription);
    }

    public async Task Remove(int id)
    {
        var subscription = _subscriptionRepository.GetById(id).Result;
        await _subscriptionRepository.Remove(subscription);
    }
}
