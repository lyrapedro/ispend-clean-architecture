using iSpend.Application.DTOs;

namespace iSpend.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetCategories(string userId);
    Task<CategoryDto> GetById(int id);
    Task<IEnumerable<CategoryDto>> GetByName(string userId, string name);
    Task Add(CategoryDto CategoryDto);
    Task Update(CategoryDto CategoryDto);
    Task Remove(CategoryDto CategoryDto);
}
