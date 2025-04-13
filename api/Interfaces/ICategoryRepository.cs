using api.Models;
using DemoShop.Helpers;
using DemoShop.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoShop.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category categoryModel);
        Task<Category?> UpdateAsync(int id, Category categoryModel);
        Task<Category?> DeleteAsync(int id);
    }
}
