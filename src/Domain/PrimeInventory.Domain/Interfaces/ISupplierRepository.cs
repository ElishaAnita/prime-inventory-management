using PrimeInventory.Domain.Entities;

namespace PrimeInventory.Domain.Interfaces;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<Supplier?> GetByNameAsync(string name);
}
