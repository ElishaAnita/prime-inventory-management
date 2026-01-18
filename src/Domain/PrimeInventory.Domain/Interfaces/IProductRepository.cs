using PrimeInventory.Domain.Entities;

namespace PrimeInventory.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
    Task<IEnumerable<Product>> GetBySupplierIdAsync(int supplierId);
    Task<Product?> GetBySkuAsync(string sku);
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold);
}
