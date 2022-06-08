using AutoMapper;
using iSpend.Application.DTOs;
using iSpend.Application.Interfaces;
using iSpend.Domain.Entities;
using iSpend.Domain.Interfaces;

namespace iSpend.Application.Services;

public class CategoryService : ICategoryService
{
    private ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategories(string userId)
    {
        var categories = await _categoryRepository.GetCategories(userId);
        return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
    }

    public async Task<IEnumerable<PurchaseDTO>> GetPurchasesFromCategory(string userId, int purchaseId)
    {
        var purchases = await _categoryRepository.GetPurchasesFromCategory(userId, purchaseId);
        return _mapper.Map<IEnumerable<PurchaseDTO>>(purchases);
    }

    public async Task<CategoryDTO> GetById(string userId, int? id)
    {
        var category = await _categoryRepository.GetById(userId, id);
        return _mapper.Map<CategoryDTO>(category);
    }

    public async Task<IEnumerable<CategoryDTO>> GetByName(string userId, string name)
    {
        IEnumerable<CategoryDTO> categories;

        if (!string.IsNullOrEmpty(name))
        {
            var query = await _categoryRepository.GetByName(userId, name);
            categories = query.Select(c => _mapper.Map<CategoryDTO>(c)).ToList();
        }
        else
        {
            categories = await GetCategories(userId);
        }

        return categories;
    }

    public async Task Add(CategoryDTO categoryDTO)
    {
        var category = _mapper.Map<Category>(categoryDTO);
        await _categoryRepository.Create(category);
    }

    public async Task Update(CategoryDTO categoryDTO)
    {
        var category = _mapper.Map<Category>(categoryDTO);
        await _categoryRepository.Update(category);
    }

    public async Task Remove(CategoryDTO categoryDTO)
    {
        var category = _mapper.Map<Category>(categoryDTO);
        await _categoryRepository.Remove(category);
    }
}
