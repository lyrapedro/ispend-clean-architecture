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

        var subscriptionsDto = _mapper.Map<IEnumerable<SubscriptionDTO>>(subscriptions);

        foreach (var dto in subscriptionsDto)
        {
            dto.Late = await HasLatePayment(dto.Id, dto.BillingDay);
        }

        return subscriptionsDto;
    }

    public async Task<IEnumerable<SubscriptionDTO>> GetSubscriptionsFromCreditCard(int creditCardId)
    {
        var subscription = await _subscriptionRepository.GetSubscriptionsFromCreditCard(creditCardId);
        return _mapper.Map<IEnumerable<SubscriptionDTO>>(subscription);
    }

    public async Task<SubscriptionDTO> GetById(int id)
    {
        var subscription = await _subscriptionRepository.GetById(id);
        bool hasPendingPayment = await HasLatePayment(id, subscription.BillingDay);

        var subscriptionDto = _mapper.Map<SubscriptionDTO>(subscription);
        subscriptionDto.Late = hasPendingPayment;

        return subscriptionDto;
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

        foreach (var subscription in subscriptions)
        {
            subscription.Late = await HasLatePayment(subscription.Id, subscription.BillingDay);
        }

        return subscriptions;
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

    public async Task Remove(SubscriptionDTO subscriptionDTO)
    {
        var subscription = _mapper.Map<Subscription>(subscriptionDTO);
        await _subscriptionRepository.Remove(subscription);
    }

    #region Util
    public async Task<bool> HasLatePayment(int subscriptionId, int billingDay)
    {
        var todayDate = DateTime.Now;
        var paymentDate = new DateTime(todayDate.Year, todayDate.Month, billingDay);

        if (todayDate.Date <= paymentDate.Date)
            return false;

        var alreadyPaid = await _subscriptionRepository.GetAlreadyPaid(subscriptionId);
        var lastPayment = alreadyPaid.OrderBy(x => x.ReferenceDate).FirstOrDefault();

        if (lastPayment != null)
        {
            var lastPaymentWasThisMonth = (lastPayment.ReferenceDate.Month == todayDate.Month && lastPayment.ReferenceDate.Year == todayDate.Year);
            if (lastPaymentWasThisMonth)
                return false;
        }

        return true;
    }
    #endregion
}
