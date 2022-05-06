using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;
using iSpend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iSpend.Infra.Data.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    ApplicationDbContext _purchaseRepository;

    public PurchaseRepository(ApplicationDbContext context)
    {
        _purchaseRepository = context;
    }

    public async Task<Purchase> Create(Purchase purchase)
    {
        _purchaseRepository.Add(purchase);
        await _purchaseRepository.SaveChangesAsync();
        return purchase;
    }

    public async Task<Purchase> GetById(int? id)
    {
        return await _purchaseRepository.Purchases.FindAsync(id);
    }

    public async Task<Purchase> GetPurchaseCreditCard(int? id)
    {
        return await _purchaseRepository.Purchases.Include(p => p.CreditCard).SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Purchase>> GetPurchases(int creditCardId)
    {
        return await _purchaseRepository.Purchases.Where(p => p.CreditCardId == creditCardId).ToListAsync();
    }

    public async Task<Purchase> Remove(Purchase purchase)
    {
        _purchaseRepository.Remove(purchase);
        await _purchaseRepository.SaveChangesAsync();
        return purchase;
    }

    public async Task<Purchase> Update(Purchase purchase)
    {
        _purchaseRepository.Update(purchase);
        await _purchaseRepository.SaveChangesAsync();
        return purchase;
    }
}
