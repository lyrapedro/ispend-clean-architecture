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

    public async Task<Purchase> GetById(int id)
    {

        return await _purchaseContext.Purchases.Include(p => p.CreditCard).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Purchase>> GetByName(string userId, string name)
    {
        return await _purchaseContext.Purchases.Include(p => p.CreditCard).Include(c => c.Category).Where(p => p.CreditCard.UserId == userId && p.Name.Contains(name)).ToListAsync();
    }

    public async Task<IEnumerable<Purchase>> GetPurchases(string userId)
    {

        return await _purchaseContext.Purchases.Include(c => c.Category).Where(p => p.CreditCard.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Purchase>> GetPurchasesFromCreditCard(int creditCardId)
    {

        return await _purchaseContext.Purchases.Include(p => p.CreditCard).Include(c => c.Category).Where(p => p.CreditCardId == creditCardId).ToListAsync();
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
