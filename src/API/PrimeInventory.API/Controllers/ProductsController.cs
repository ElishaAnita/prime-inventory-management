using Microsoft.AspNetCore.Mvc;
using PrimeInventory.ApplicationCore.DTOs;
using PrimeInventory.ApplicationCore.Interfaces;

namespace PrimeInventory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();
        
        return Ok(product);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int categoryId)
    {
        var products = await _productService.GetProductsByCategoryAsync(categoryId);
        return Ok(products);
    }

    [HttpGet("supplier/{supplierId}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsBySupplier(int supplierId)
    {
        var products = await _productService.GetProductsBySupplierAsync(supplierId);
        return Ok(products);
    }

    [HttpGet("low-stock/{threshold}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetLowStockProducts(int threshold)
    {
        var products = await _productService.GetLowStockProductsAsync(threshold);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto productDto)
    {
        var createdProduct = await _productService.CreateProductAsync(productDto);
        return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
    {
        if (id != productDto.Id)
            return BadRequest();

        await _productService.UpdateProductAsync(productDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}
