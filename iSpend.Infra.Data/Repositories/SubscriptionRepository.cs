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

    public async Task<Subscription> CreateAsync(Subscription subscription)
    {
        _subscriptionContext.Add(subscription);
        await _subscriptionContext.SaveChangesAsync();
        return subscription;
    }

    public async Task<Subscription> GetSubscriptionByIdAsync(int? id)
    {
        return await _subscriptionContext.Subscriptions.FindAsync(id);
    }

    public async Task<Subscription> GetSubscriptionCreditCardAsync(int? id)
    {
        return await _subscriptionContext.Subscriptions.Include(s => s.CreditCard).SingleOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Subscription>> GetSubscriptionsAsync(int creditCardId)
    {
        return await _subscriptionContext.Subscriptions.Where(s => s.CreditCardId == creditCardId).ToListAsync();
    }

    public async Task<Subscription> RemoveAsync(Subscription subscription)
    {
        _subscriptionContext.Remove(subscription);
        await _subscriptionContext.SaveChangesAsync();
        return subscription;
    }

    public async Task<Subscription> UpdateAsync(Subscription subscription)
    {
        _subscriptionContext.Update(subscription);
        await _subscriptionContext.SaveChangesAsync();
        return subscription;
    }
}
