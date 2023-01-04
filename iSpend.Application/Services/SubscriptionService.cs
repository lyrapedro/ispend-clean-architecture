using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private ISubscriptionRepository _subscriptionRepository;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<IEnumerable<SubscriptionDto>> GetSubscriptions(string userId)
    {
        var subscriptions = await _subscriptionRepository.GetSubscriptions(userId);

        var subscriptionsDto = subscriptions.Select(s => (SubscriptionDto)s);

        foreach (var dto in subscriptionsDto)
        {
            dto.Late = await HasLatePayment(dto.Id, dto.BillingDay);
        }

        return subscriptionsDto;
    }

    public async Task<IEnumerable<SubscriptionDto>> GetSubscriptionsFromCreditCard(int creditCardId)
    {
        var subscriptions = await _subscriptionRepository.GetSubscriptionsFromCreditCard(creditCardId);
        return subscriptions.Select(s => (SubscriptionDto)s);
    }

    public async Task<SubscriptionDto> GetById(int id)
    {
        var subscription = await _subscriptionRepository.GetById(id);
        bool hasPendingPayment = await HasLatePayment(id, subscription.BillingDay);

        var subscriptionDto = (SubscriptionDto)subscription;
        subscriptionDto.Late = hasPendingPayment;

        return subscriptionDto;
    }

    public async Task<IEnumerable<SubscriptionDto>> GetByName(string userId, string name)
    {
        IEnumerable<SubscriptionDto> subscriptions;

        if (!string.IsNullOrEmpty(name))
        {
            var query = await _subscriptionRepository.GetByName(userId, name);
            subscriptions = query.Select(s => (SubscriptionDto)s);
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

    public async Task Add(SubscriptionDto subscriptionDto)
    {
        var subscription = (Subscription)subscriptionDto;
        await _subscriptionRepository.Create(subscription);
    }

    public async Task Update(SubscriptionDto subscriptionDto)
    {
        var subscription = (Subscription)subscriptionDto;
        await _subscriptionRepository.Update(subscription);
    }

    public async Task Remove(SubscriptionDto subscriptionDto)
    {
        var subscription = (Subscription)subscriptionDto;
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
            var lastPaymentWasThisMonth = (lastPayment.ReferenceDate.Month == todayDate.Month &&
                                           lastPayment.ReferenceDate.Year == todayDate.Year);
            if (lastPaymentWasThisMonth)
                return false;
        }

        return true;
    }

    #endregion
}