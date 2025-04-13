using DemoShop.Dtos.Product;
using DemoShop.Dtos.User;
using DemoShop.Models;

namespace DemoShop.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> CreateAsync(AppUser userModel);
        Task<AppUser?> UpdateAsync(int id, UserDto userDto);
        Task<AppUser?> DeleteAsync(int id);
    }
}
