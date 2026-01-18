using PrimeInventory.ApplicationCore.DTOs;

namespace PrimeInventory.ApplicationCore.Interfaces;

public interface ISupplierService
{
    Task<SupplierDto?> GetSupplierByIdAsync(int id);
    Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();
    Task<SupplierDto> CreateSupplierAsync(SupplierDto supplierDto);
    Task UpdateSupplierAsync(SupplierDto supplierDto);
    Task DeleteSupplierAsync(int id);
}
