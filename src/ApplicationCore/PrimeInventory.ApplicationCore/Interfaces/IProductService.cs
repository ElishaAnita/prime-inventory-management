using PrimeInventory.ApplicationCore.DTOs;

namespace PrimeInventory.ApplicationCore.Interfaces;

public interface IProductService
{
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
    Task<IEnumerable<ProductDto>> GetProductsBySupplierAsync(int supplierId);
    Task<IEnumerable<ProductDto>> GetLowStockProductsAsync(int threshold);
    Task<ProductDto> CreateProductAsync(ProductDto productDto);
    Task UpdateProductAsync(ProductDto productDto);
    Task DeleteProductAsync(int id);
}
