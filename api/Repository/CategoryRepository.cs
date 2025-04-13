using DemoShop.Data;
using DemoShop.Interfaces;
using DemoShop.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoShop.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category categoryModel)
        {
            await _context.Categories.AddAsync(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var categoryModel = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            
            if (categoryModel == null)
            {
                return null;
            }

            _context.Categories.Remove(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(s => s.Products).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.Include(s => s.Products).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateAsync(int id, Category categoryModel)
        {
            var existingCategory = await _context.Categories.FindAsync(id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.CategoryName = categoryModel.CategoryName;
            await _context.SaveChangesAsync();
            return existingCategory;
        }
    }
}
