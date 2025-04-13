using DemoShop.Models;

namespace DemoShop.Interfaces
{
    public interface IFavouriteRepository
    {
        Task<List<Product>> GetUserFavourite(AppUser user);
        Task<Favourite> CreateAsync(Favourite favourite);
        Task<Favourite> DeleteFavourite(AppUser appUser, string title);
    }
}
