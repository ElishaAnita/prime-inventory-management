using PrimeInventory.ApplicationCore.DTOs;

namespace PrimeInventory.ApplicationCore.Interfaces;

public interface ICategoryService
{
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto);
    Task UpdateCategoryAsync(CategoryDto categoryDto);
    Task DeleteCategoryAsync(int id);
}
