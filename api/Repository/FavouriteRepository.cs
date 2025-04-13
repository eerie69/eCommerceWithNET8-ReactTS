using DemoShop.Data;
using DemoShop.Interfaces;
using DemoShop.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoShop.Repository
{
    public class FavouriteRepository : IFavouriteRepository
    {
        private readonly ApplicationDbContext _context;
        public FavouriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Favourite> CreateAsync(Favourite favourite)
        {
            await _context.Favorites.AddAsync(favourite);
            await _context.SaveChangesAsync();
            return favourite;
        }

        public async Task<Favourite> DeleteFavourite(AppUser appUser, string title)
        {
            var favouriteModel = await _context.Favorites.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Product.Title.ToLower() == title.ToLower());

            if (favouriteModel == null)
            {
                return null;
            }

            _context.Favorites.Remove(favouriteModel);
            await _context.SaveChangesAsync();
            return favouriteModel;
        }

        public async Task<List<Product>> GetUserFavourite(AppUser user)
        {
            return await _context.Favorites.Where(u => u.AppUserId == user.Id)
            .Select(product => new Product
            {
                Id = product.ProductId,
                Title = product.Product.Title,
                Price = product.Product.Price,
                Image = product.Product.Image,
                Featured = product.Product.Featured,
                DateUploaded = product.Product.DateUploaded
            }).ToListAsync();
        }
    }
}
