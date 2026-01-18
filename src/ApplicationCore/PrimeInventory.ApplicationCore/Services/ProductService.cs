using PrimeInventory.ApplicationCore.DTOs;
using PrimeInventory.ApplicationCore.Interfaces;
using PrimeInventory.Domain.Entities;
using PrimeInventory.Domain.Interfaces;

namespace PrimeInventory.ApplicationCore.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product == null ? null : MapToDto(product);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(MapToDto);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _productRepository.GetByCategoryIdAsync(categoryId);
        return products.Select(MapToDto);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsBySupplierAsync(int supplierId)
    {
        var products = await _productRepository.GetBySupplierIdAsync(supplierId);
        return products.Select(MapToDto);
    }

    public async Task<IEnumerable<ProductDto>> GetLowStockProductsAsync(int threshold)
    {
        var products = await _productRepository.GetLowStockProductsAsync(threshold);
        return products.Select(MapToDto);
    }

    public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
    {
        var product = MapToEntity(productDto);
        product.CreatedAt = DateTime.UtcNow;
        var createdProduct = await _productRepository.AddAsync(product);
        return MapToDto(createdProduct);
    }

    public async Task UpdateProductAsync(ProductDto productDto)
    {
        var product = MapToEntity(productDto);
        product.UpdatedAt = DateTime.UtcNow;
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Sku = product.Sku,
            Price = product.Price,
            QuantityInStock = product.QuantityInStock,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name,
            SupplierId = product.SupplierId,
            SupplierName = product.Supplier?.Name
        };
    }

    private static Product MapToEntity(ProductDto dto)
    {
        return new Product
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Sku = dto.Sku,
            Price = dto.Price,
            QuantityInStock = dto.QuantityInStock,
            CategoryId = dto.CategoryId,
            SupplierId = dto.SupplierId
        };
    }
}
