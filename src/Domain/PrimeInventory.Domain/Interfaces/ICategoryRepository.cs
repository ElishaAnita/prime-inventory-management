using PrimeInventory.Domain.Entities;

namespace PrimeInventory.Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
}
