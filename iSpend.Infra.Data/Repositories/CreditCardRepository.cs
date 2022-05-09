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

    public async Task<CreditCard> CreateAsync(CreditCard creditCard)
    {
        _creditCardContext.Add(creditCard);
        await _creditCardContext.SaveChangesAsync();
        return creditCard;
    }

    public async Task<CreditCard> GetByIdAsync(int? id)
    {
        return await _creditCardContext.CreditCards.FindAsync(id);
    }

    public async Task<IEnumerable<CreditCard>> GetCreditCardsAsync(string userId)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _creditCardContext.CreditCards.Where(c => c.UserId == validGuid).ToListAsync();
    }

    public async Task<CreditCard> RemoveAsync(CreditCard creditCard)
    {
        _creditCardContext.Remove(creditCard);
        await _creditCardContext.SaveChangesAsync();
        return creditCard;
    }

    public async Task<CreditCard> UpdateAsync(CreditCard creditCard)
    {
        _creditCardContext.Update(creditCard);
        await _creditCardContext.SaveChangesAsync();
        return creditCard;
    }
}
