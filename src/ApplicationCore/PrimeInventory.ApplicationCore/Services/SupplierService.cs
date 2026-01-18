using PrimeInventory.ApplicationCore.DTOs;
using PrimeInventory.ApplicationCore.Interfaces;
using PrimeInventory.Domain.Entities;
using PrimeInventory.Domain.Interfaces;

namespace PrimeInventory.ApplicationCore.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<SupplierDto?> GetSupplierByIdAsync(int id)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id);
        return supplier == null ? null : MapToDto(supplier);
    }

    public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
    {
        var suppliers = await _supplierRepository.GetAllAsync();
        return suppliers.Select(MapToDto);
    }

    public async Task<SupplierDto> CreateSupplierAsync(SupplierDto supplierDto)
    {
        var supplier = MapToEntity(supplierDto);
        supplier.CreatedAt = DateTime.UtcNow;
        var createdSupplier = await _supplierRepository.AddAsync(supplier);
        return MapToDto(createdSupplier);
    }

    public async Task UpdateSupplierAsync(SupplierDto supplierDto)
    {
        var supplier = MapToEntity(supplierDto);
        supplier.UpdatedAt = DateTime.UtcNow;
        await _supplierRepository.UpdateAsync(supplier);
    }

    public async Task DeleteSupplierAsync(int id)
    {
        await _supplierRepository.DeleteAsync(id);
    }

    private static SupplierDto MapToDto(Supplier supplier)
    {
        return new SupplierDto
        {
            Id = supplier.Id,
            Name = supplier.Name,
            ContactName = supplier.ContactName,
            Email = supplier.Email,
            Phone = supplier.Phone,
            Address = supplier.Address,
            ProductCount = supplier.Products?.Count ?? 0
        };
    }

    private static Supplier MapToEntity(SupplierDto dto)
    {
        return new Supplier
        {
            Id = dto.Id,
            Name = dto.Name,
            ContactName = dto.ContactName,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address
        };
    }
}
