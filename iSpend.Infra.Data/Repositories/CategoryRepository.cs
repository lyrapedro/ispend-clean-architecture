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

    public async Task<Category> GetById(int id)
    {

        return await _categoryContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Category>> GetByName(string userId, string name)
    {
        return await _categoryContext.Categories.Where(c => c.UserId == userId && c.Name.Contains(name)).ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetCategories(string userId)
    {

        return await _categoryContext.Categories.Where(c => c.UserId == null || c.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Purchase>> GetPurchasesFromCategory(string userId, int categoryId)
    {
        return await _categoryContext.Purchases.Where(p => p.CreditCard.UserId == userId && p.CategoryId == categoryId).ToListAsync();
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
