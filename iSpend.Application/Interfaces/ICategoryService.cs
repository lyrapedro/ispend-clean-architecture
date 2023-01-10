using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategories(string userId);
    Task<CategoryDto> GetById(int id);
    Task<IEnumerable<CategoryDto>> GetByName(string userId, string name);
    Task Add(CategoryDto categoryDto);
    Task Update(CategoryDto categoryDto);
    Task Remove(CategoryDto categoryDto);
}
