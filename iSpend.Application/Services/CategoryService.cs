using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategories(string userId)
    {
        var categories = await _categoryRepository.GetCategories(userId);
        return categories.Select(c => (CategoryDto)c);
    }

    public async Task<CategoryDto> GetById(int id)
    {
        var category = await _categoryRepository.GetById(id);
        return (CategoryDto)category;
    }

    public async Task<IEnumerable<CategoryDto>> GetByName(string userId, string name)
    {
        IEnumerable<CategoryDto> categories;

        if (!string.IsNullOrEmpty(name))
        {
            var query = await _categoryRepository.GetByName(userId, name);
            categories = query.Select(c => (CategoryDto)c);
        }
        else
        {
            categories = await GetCategories(userId);
        }

        return categories;
    }

    public async Task Add(CategoryDto categoryDto)
    {
        var category = (Category)categoryDto;
        await _categoryRepository.Create(category);
    }

    public async Task Update(CategoryDto categoryDto)
    {
        categoryDto.ModifiedAt = DateTime.Now;
        var category = (Category)categoryDto;
        await _categoryRepository.Update(category);
    }

    public async Task Remove(CategoryDto categoryDto)
    {
        var category = (Category)categoryDto;
        await _categoryRepository.Remove(category);
    }
}