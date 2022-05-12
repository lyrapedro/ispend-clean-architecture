using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;
using iSpend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iSpend.Infra.Data.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    ApplicationDbContext _subscriptionContext;

    public SubscriptionRepository(ApplicationDbContext context)
    {
        _subscriptionContext = context;
    }

    public async Task<Subscription> GetById(string userId, int? id)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _subscriptionContext.Subscriptions.Include(p => p.CreditCard).FirstOrDefaultAsync(p => p.Id == id && p.CreditCard.UserId == validGuid);
    }

    public async Task<IEnumerable<Subscription>> GetSubscriptionsFromCreditCard(string userId, int creditCardId)
    {
        Guid validGuid = Guid.Parse(userId);

        return await _subscriptionContext.Subscriptions.Include(s => s.CreditCard).Where(s => s.CreditCardId == creditCardId && s.CreditCard.UserId == validGuid).ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> GetByName(string userId, string name)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _subscriptionContext.Subscriptions.Include(s => s.CreditCard).Where(s => s.CreditCard.UserId == validGuid && s.Name.Contains(name)).ToListAsync();
    }

    public async Task<CreditCard> GetSubscriptionCreditCard(string userId, int? id)
    {
        Guid validGuid = Guid.Parse(userId);

        var subscription = _subscriptionContext.Subscriptions.Include(s => s.CreditCard).FirstOrDefaultAsync(s => s.CreditCard.UserId == validGuid && s.Id == id);
        return subscription.Result.CreditCard;
    }

    public async Task<IEnumerable<Subscription>> GetSubscriptions(string userId)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _subscriptionContext.Subscriptions.Include(s => s.CreditCard).Where(s => s.CreditCard.UserId == validGuid).ToListAsync();
    }

    public async Task<Subscription> Create(Subscription subscription)
    {
        _subscriptionContext.Add(subscription);
        await _subscriptionContext.SaveChangesAsync();
        return subscription;
    }

    public async Task<Subscription> Update(Subscription subscription)
    {
        _subscriptionContext.Update(subscription);
        await _subscriptionContext.SaveChangesAsync();
        return subscription;
    }

    public async Task<Subscription> Remove(Subscription subscription)
    {
        _subscriptionContext.Remove(subscription);
        await _subscriptionContext.SaveChangesAsync();
        return subscription;
    }
}
