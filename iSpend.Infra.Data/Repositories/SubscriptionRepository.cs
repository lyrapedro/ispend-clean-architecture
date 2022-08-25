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

    public async Task<Subscription> GetById(int id)
    {
        return await _subscriptionContext.Subscriptions.Include(p => p.CreditCard).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Subscription>> GetSubscriptionsFromCreditCard(int creditCardId)
    {

        return await _subscriptionContext.Subscriptions.Include(s => s.CreditCard).Where(s => s.CreditCardId == creditCardId).ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> GetByName(string userId, string name)
    {
        return await _subscriptionContext.Subscriptions.Include(s => s.CreditCard).Where(s => s.CreditCard.UserId == userId && s.Name.Contains(name)).ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> GetSubscriptions(string userId)
    {
        return await _subscriptionContext.Subscriptions.Include(s => s.CreditCard).Where(s => s.CreditCard.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<SubscriptionNode>> GetAlreadyPaid(int subscriptionId)
    {
        return await _subscriptionContext.SubscriptionNodes.Include(s => s.Subscription).Where(s => s.SubscriptionId == subscriptionId && s.Paid == true).ToListAsync();
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
