using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetCategories(string userId);
    Task<CategoryDTO> GetById(int id);
    Task<IEnumerable<CategoryDTO>> GetByName(string userId, string name);
    Task Add(CategoryDTO categoryDTO);
    Task Update(CategoryDTO categoryDTO);
    Task Remove(CategoryDTO categoryDTO);
}
