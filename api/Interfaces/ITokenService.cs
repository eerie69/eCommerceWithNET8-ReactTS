using DemoShop.Models;

namespace DemoShop.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
