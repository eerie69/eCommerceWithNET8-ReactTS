using Microsoft.AspNetCore.Identity;

namespace DemoShop.Models
{
    public class AppUser : IdentityUser
    {
        public string ProfileImageUrl { get; set; } = string.Empty;
        public List<Favourite> Favourites { get; set; } = new List<Favourite>();
    }
}
