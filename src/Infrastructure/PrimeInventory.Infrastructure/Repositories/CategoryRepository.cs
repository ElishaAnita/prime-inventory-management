using Microsoft.EntityFrameworkCore;
using PrimeInventory.Domain.Entities;
using PrimeInventory.Domain.Interfaces;
using PrimeInventory.Infrastructure.Data;

namespace PrimeInventory.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(InventoryDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _dbSet
            .Include(c => c.Products)
            .ToListAsync();
    }

    public override async Task<Category?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _dbSet
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Name == name);
    }
}
