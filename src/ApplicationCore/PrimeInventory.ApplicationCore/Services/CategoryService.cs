using PrimeInventory.ApplicationCore.DTOs;
using PrimeInventory.ApplicationCore.Interfaces;
using PrimeInventory.Domain.Entities;
using PrimeInventory.Domain.Interfaces;

namespace PrimeInventory.ApplicationCore.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return category == null ? null : MapToDto(category);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(MapToDto);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto)
    {
        var category = MapToEntity(categoryDto);
        category.CreatedAt = DateTime.UtcNow;
        var createdCategory = await _categoryRepository.AddAsync(category);
        return MapToDto(createdCategory);
    }

    public async Task UpdateCategoryAsync(CategoryDto categoryDto)
    {
        var category = MapToEntity(categoryDto);
        category.UpdatedAt = DateTime.UtcNow;
        await _categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await _categoryRepository.DeleteAsync(id);
    }

    private static CategoryDto MapToDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ProductCount = category.Products?.Count ?? 0
        };
    }

    private static Category MapToEntity(CategoryDto dto)
    {
        return new Category
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description
        };
    }
}
