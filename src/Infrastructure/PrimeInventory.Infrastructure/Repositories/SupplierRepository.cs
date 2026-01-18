using Microsoft.EntityFrameworkCore;
using PrimeInventory.Domain.Entities;
using PrimeInventory.Domain.Interfaces;
using PrimeInventory.Infrastructure.Data;

namespace PrimeInventory.Infrastructure.Repositories;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    public SupplierRepository(InventoryDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Supplier>> GetAllAsync()
    {
        return await _dbSet
            .Include(s => s.Products)
            .ToListAsync();
    }

    public override async Task<Supplier?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(s => s.Products)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Supplier?> GetByNameAsync(string name)
    {
        return await _dbSet
            .Include(s => s.Products)
            .FirstOrDefaultAsync(s => s.Name == name);
    }
}
