using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;
using iSpend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iSpend.Infra.Data.Repositories;

public class CreditCardRepository : ICreditCardRepository
{
    ApplicationDbContext _creditCardContext;
    public CreditCardRepository(ApplicationDbContext context)
    {
        _creditCardContext = context;
    }

    public async Task<CreditCard> Create(CreditCard creditCard)
    {
        _creditCardContext.Add(creditCard);
        await _creditCardContext.SaveChangesAsync();
        return creditCard;
    }

    public async Task<CreditCard> GetById(int id)
    {
        return await _creditCardContext.CreditCards.Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CreditCard>> GetByName(string userId, string name)
    {
        return await _creditCardContext.CreditCards.Where(c => c.UserId == userId && c.Name.Contains(name)).ToListAsync();
    }

    public async Task<CreditCard> GetCreditCardFromSubscription(int subscriptionId)
    {
        var subscription = await _creditCardContext.Subscriptions.Include(s => s.CreditCard).FirstOrDefaultAsync(s => s.Id == subscriptionId);

        if (subscription == null)
            return null;

        return subscription.CreditCard;
    }

    public async Task<CreditCard> GetCreditCardFromPurchase(int purchaseId)
    {
        var purchase = await _creditCardContext.Purchases.Include(s => s.CreditCard).FirstOrDefaultAsync(s => s.Id == purchaseId);

        if (purchase == null)
            return null;

        return purchase.CreditCard;
    }

    public async Task<IEnumerable<CreditCard>> GetCreditCards(string userId)
    {
        return await _creditCardContext.CreditCards.Where(c => c.UserId == userId).ToListAsync();
    }

    public async Task<CreditCard> Remove(CreditCard creditCard)
    {
        _creditCardContext.Remove(creditCard);
        await _creditCardContext.SaveChangesAsync();
        return creditCard;
    }

    public async Task<CreditCard> Update(CreditCard creditCard)
    {
        _creditCardContext.Update(creditCard);
        await _creditCardContext.SaveChangesAsync();
        return creditCard;
    }
}
