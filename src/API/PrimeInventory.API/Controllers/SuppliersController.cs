using Microsoft.AspNetCore.Mvc;
using PrimeInventory.ApplicationCore.DTOs;
using PrimeInventory.ApplicationCore.Interfaces;

namespace PrimeInventory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliers()
    {
        var suppliers = await _supplierService.GetAllSuppliersAsync();
        return Ok(suppliers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierDto>> GetSupplier(int id)
    {
        var supplier = await _supplierService.GetSupplierByIdAsync(id);
        if (supplier == null)
            return NotFound();
        
        return Ok(supplier);
    }

    [HttpPost]
    public async Task<ActionResult<SupplierDto>> CreateSupplier(SupplierDto supplierDto)
    {
        var createdSupplier = await _supplierService.CreateSupplierAsync(supplierDto);
        return CreatedAtAction(nameof(GetSupplier), new { id = createdSupplier.Id }, createdSupplier);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSupplier(int id, SupplierDto supplierDto)
    {
        if (id != supplierDto.Id)
            return BadRequest();

        await _supplierService.UpdateSupplierAsync(supplierDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupplier(int id)
    {
        await _supplierService.DeleteSupplierAsync(id);
        return NoContent();
    }
}
