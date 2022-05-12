using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;
using iSpend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iSpend.Infra.Data.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    ApplicationDbContext _purchaseContext;

    public PurchaseRepository(ApplicationDbContext context)
    {
        _purchaseContext = context;
    }

    public async Task<Purchase> GetById(string userId, int? id)
    {
        Guid validGuid = Guid.Parse(userId);

        return await _purchaseContext.Purchases.Include(p => p.CreditCard).FirstOrDefaultAsync(p => p.Id == id && p.CreditCard.UserId == validGuid);
    }

    public async Task<IEnumerable<Purchase>> GetByName(string userId, string name)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _purchaseContext.Purchases.Include(p => p.CreditCard).Where(p => p.CreditCard.UserId == validGuid && p.Name.Contains(name)).ToListAsync();
    }

    public async Task<IEnumerable<Purchase>> GetPurchases(string userId)
    {
        Guid validGuid = Guid.Parse(userId);

        return await _purchaseContext.Purchases.Where(p => p.CreditCard.UserId == validGuid).ToListAsync();
    }

    public async Task<IEnumerable<Purchase>> GetPurchasesFromCreditCard(string userId, int creditCardId)
    {
        Guid validGuid = Guid.Parse(userId);

        return await _purchaseContext.Purchases.Include(p => p.CreditCard).Where(p => p.CreditCardId == creditCardId && p.CreditCard.UserId == validGuid).ToListAsync();
    }

    public async Task<CreditCard> GetPurchaseCreditCard(string userId, int? id)
    {
        Guid validGuid = Guid.Parse(userId);

        var purchase = _purchaseContext.Purchases.Include(p => p.CreditCard).FirstOrDefaultAsync(p => p.CreditCard.UserId == validGuid && p.Id == id);
        return purchase.Result.CreditCard;
    }

    public async Task<Purchase> Create(Purchase purchase)
    {
        _purchaseContext.Add(purchase);
        await _purchaseContext.SaveChangesAsync();
        return purchase;
    }

    public async Task<Purchase> Update(Purchase purchase)
    {
        _purchaseContext.Update(purchase);
        await _purchaseContext.SaveChangesAsync();
        return purchase;
    }

    public async Task<Purchase> Remove(Purchase purchase)
    {
        _purchaseContext.Remove(purchase);
        await _purchaseContext.SaveChangesAsync();
        return purchase;
    }
}
