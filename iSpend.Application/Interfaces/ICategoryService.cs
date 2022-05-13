using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetCategories(string userId);
    Task<IEnumerable<PurchaseDTO>> GetPurchasesFromCategory(string userId, int categoryId);
    Task<CategoryDTO> GetById(string userId, int? id);
    Task<IEnumerable<CategoryDTO>> GetByName(string userId, string name);
    Task Add(CategoryDTO categoryDTO);
    Task Update(CategoryDTO categoryDTO);
    Task Remove(string userId, int? id);
}
