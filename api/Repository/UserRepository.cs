using DemoShop.Data;
using DemoShop.Dtos.Product;
using DemoShop.Dtos.User;
using DemoShop.Interfaces;
using DemoShop.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoShop.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<AppUser> CreateAsync(AppUser userModel)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public Task<AppUser?> UpdateAsync(int id, UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
