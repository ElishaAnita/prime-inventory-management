using Microsoft.EntityFrameworkCore;
using PrimeInventory.Domain.Entities;
using PrimeInventory.Domain.Interfaces;
using PrimeInventory.Infrastructure.Data;

namespace PrimeInventory.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(InventoryDbContext context) : base(context)
    {
    }

    public override async Task<Product?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetBySupplierIdAsync(int supplierId)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(p => p.SupplierId == supplierId)
            .ToListAsync();
    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.Sku == sku);
    }

    public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(p => p.QuantityInStock <= threshold)
            .ToListAsync();
    }
}
