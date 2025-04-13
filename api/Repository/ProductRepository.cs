using CloudinaryDotNet.Actions;
using DemoShop.Data;
using DemoShop.Dtos.Product;
using DemoShop.Helpers;
using DemoShop.Interfaces;
using DemoShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IO;

namespace DemoShop.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;

        public ProductRepository(ApplicationDbContext context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        public async Task<Product> CreateAsync(Product productModel)
        {
            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var productModel = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (productModel == null)
            {
                return null;
            }

            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
            return (productModel);
        }

        public async Task<List<Product>> GetAllAsync(QueryObject query)
        {
            var products = _context.Products
                .AsNoTracking()
                .Include(c => c.Reviews)
                    .ThenInclude(a => a.AppUser)
                .Include(c => c.Category)
                .Include(s => s.Stock)
                .Include(c => c.CartDetail)
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                string titleLower = query.Title.ToLower();
                products = products.Where(s => s.Title.ToLower().Contains(query.Title));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDecsending ? products.OrderByDescending(s => s.Title) : products.OrderBy(s => s.Title);
                }
            }

            int pageSize = query.PageSize > 0 ? query.PageSize : 10; // Значение по умолчанию
            int pageNumber = query.PageNumber > 0 ? query.PageNumber : 1;

            var skipNumber = (pageNumber - 1) * pageSize;

            return await products.Skip(skipNumber).Take(pageSize).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(c => c.Reviews)
                .Include(c => c.Category)
                .Include(c =>c.Stock)
                .Include(c => c.CartDetail)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Product> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Products.Include(c => c.Reviews).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Product?> GetByTitleAsync(string title)
        {
            return await _context.Products.FirstOrDefaultAsync(s => s.Title == title);
        }

        public async Task<Product?> UpdateAsync(int id, UpdateProductRequestDto productDto)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProduct == null)
                return null;

            try
            {
                await _photoService.DeletePhotoAsync(existingProduct.Image);
            }
            catch (Exception ex)
            {

            }

            var photoResult = await _photoService.AddPhotoAsync(productDto.Image);
            if (photoResult is null)
                throw new InvalidOperationException("invalid photo Upload");

            existingProduct.Title = productDto.Title;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            existingProduct.Image = photoResult.Url.ToString();
            existingProduct.Featured = productDto.Featured;
            

            await _context.SaveChangesAsync();

            return existingProduct;
        }
    }
}
