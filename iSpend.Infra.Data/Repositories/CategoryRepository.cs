using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;
using iSpend.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace iSpend.Infra.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    ApplicationDbContext _categoryContext;

    public CategoryRepository(ApplicationDbContext context)
    {
        _categoryContext = context;
    }

    public async Task<Category> GetById(string userId, int? id)
    {
        Guid validGuid = Guid.Parse(userId);

        return await _categoryContext.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == validGuid);
    }

    public async Task<IEnumerable<Category>> GetByName(string userId, string name)
    {
        Guid validGuid = Guid.Parse(userId);
        return await _categoryContext.Categories.Where(c => c.UserId == validGuid && c.Name.Contains(name)).ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetCategories(string userId)
    {
        Guid validGuid = Guid.Parse(userId);

        return await _categoryContext.Categories.Where(c => c.UserId == validGuid).ToListAsync();
    }

    public async Task<IEnumerable<Purchase>> GetPurchasesFromCategory(string userId, int categoryId)
    {
        Guid validGuid = Guid.Parse(userId);

        return await _categoryContext.Purchases.Where(p => p.CreditCard.UserId == validGuid && p.CategoryId == categoryId).ToListAsync();
    }

    public async Task<Category> Create(Category category)
    {
        _categoryContext.Add(category);
        await _categoryContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category> Update(Category category)
    {
        _categoryContext.Update(category);
        await _categoryContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category> Remove(Category category)
    {
        _categoryContext.Remove(category);
        await _categoryContext.SaveChangesAsync();
        return category;
    }
}
