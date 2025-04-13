using DemoShop.Dtos.Product;
using DemoShop.Helpers;
using DemoShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoShop.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(QueryObject query);
        Task<Product?> GetByIdAsync(int id);
        Task<Product> GetByIdAsyncNoTracking(int id);
        Task<Product?> GetByTitleAsync(string title);
        Task<Product> CreateAsync(Product productModel);
        Task<Product?> UpdateAsync(int id, UpdateProductRequestDto productDto);
        Task<Product?> DeleteAsync(int id);
    }
}
