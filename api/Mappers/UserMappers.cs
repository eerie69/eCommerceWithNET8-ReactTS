using DemoShop.Dtos.Product;
using DemoShop.Dtos.User;
using DemoShop.Models;

namespace DemoShop.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this AppUser userModel)
        {
            return new UserDto
            {
                Id = userModel.Id,
                UserName = userModel.UserName,
                Image = userModel.ProfileImageUrl ?? "Unknown"
            };
        }
    }
}
