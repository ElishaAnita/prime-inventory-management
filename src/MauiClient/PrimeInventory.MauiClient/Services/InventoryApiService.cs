using System.Net.Http.Json;
using PrimeInventory.ApplicationCore.DTOs;

namespace PrimeInventory.MauiClient.Services;

public class InventoryApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public InventoryApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // Base URL should be configured via appsettings or environment variables
        // Default to localhost for development
        _baseUrl = "https://localhost:7272/api";
    }

    // Products
    public async Task<List<ProductDto>> GetProductsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ProductDto>>($"{_baseUrl}/products") ?? new List<ProductDto>();
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductDto>($"{_baseUrl}/products/{id}");
    }

    public async Task<ProductDto> CreateProductAsync(ProductDto product)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/products", product);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDto>() ?? product;
    }

    public async Task UpdateProductAsync(ProductDto product)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/products/{product.Id}", product);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteProductAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/products/{id}");
        response.EnsureSuccessStatusCode();
    }

    // Categories
    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CategoryDto>>($"{_baseUrl}/categories") ?? new List<CategoryDto>();
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<CategoryDto>($"{_baseUrl}/categories/{id}");
    }

    public async Task<CategoryDto> CreateCategoryAsync(CategoryDto category)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/categories", category);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CategoryDto>() ?? category;
    }

    public async Task UpdateCategoryAsync(CategoryDto category)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/categories/{category.Id}", category);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/categories/{id}");
        response.EnsureSuccessStatusCode();
    }

    // Suppliers
    public async Task<List<SupplierDto>> GetSuppliersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<SupplierDto>>($"{_baseUrl}/suppliers") ?? new List<SupplierDto>();
    }

    public async Task<SupplierDto?> GetSupplierByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<SupplierDto>($"{_baseUrl}/suppliers/{id}");
    }

    public async Task<SupplierDto> CreateSupplierAsync(SupplierDto supplier)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/suppliers", supplier);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SupplierDto>() ?? supplier;
    }

    public async Task UpdateSupplierAsync(SupplierDto supplier)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/suppliers/{supplier.Id}", supplier);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteSupplierAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/suppliers/{id}");
        response.EnsureSuccessStatusCode();
    }
}
