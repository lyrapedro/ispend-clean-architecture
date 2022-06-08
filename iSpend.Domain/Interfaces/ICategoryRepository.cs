using iSpend.Domain.Entities;

namespace iSpend.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategories(string userId);
    Task<IEnumerable<Purchase>> GetPurchasesFromCategory(string userId, int categoryId);
    Task<Category> GetById(int id);
    Task<IEnumerable<Category>> GetByName(string userId, string name);
    Task<Category> Create(Category category);
    Task<Category> Update(Category category);
    Task<Category> Remove(Category category);
}
