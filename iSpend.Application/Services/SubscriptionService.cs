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

    public async Task<IEnumerable<SubscriptionDTO>> GetSubscriptions(int creditCardId)
    {
        var subscriptions = await _subscriptionRepository.GetSubscriptions(creditCardId);
        return _mapper.Map<IEnumerable<SubscriptionDTO>>(subscriptions);
    }

    public async Task<SubscriptionDTO> GetById(int? id)
    {
        var subscription = await _subscriptionRepository.GetById(id);
        return _mapper.Map<SubscriptionDTO>(subscription);
    }

    public async Task<SubscriptionDTO> GetSubscriptionCreditCard(int? id)
    {
        var subscription = await _subscriptionRepository.GetSubscriptionCreditCard(id);
        return _mapper.Map<SubscriptionDTO>(subscription);
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

    public async Task Remove(int? id)
    {
        var subscription = _subscriptionRepository.GetById(id).Result;
        await _subscriptionRepository.Remove(subscription);
    }
}
