using DemoShop.Dtos.User;
using DemoShop.Interfaces;
using DemoShop.Models;
using DemoShop.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DemoShop.Extensions;

namespace DemoShop.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepo;
        private readonly IPhotoService _photoService;

        public UserController(UserManager<AppUser> userManager, IUserRepository userRepo, IPhotoService photoService)
        {
            _userManager = userManager;
            _userRepo = userRepo;
            _photoService = photoService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = await _userRepo.GetAllUsers();
            if (users == null) return BadRequest("Users not found");

            var userDto = users.Select(s => s.ToUserDto()).ToList();

            return Ok(userDto);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepo.GetUserById(id);
            if (user == null) return BadRequest("User not found");

            
            return Ok(user.ToUserDto());
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditProfile([FromForm] EditProfileDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var user = await _userManager.GetUserAsync(User);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null)
            {
                return BadRequest("User not found");
            }

            if (userDto.Image != null) // only update profile image
            {
                var photoResult = await _photoService.AddPhotoAsync(userDto.Image);

                if (photoResult.Error != null)
                {
                    return BadRequest("Failed to upload image");
                }

                if (!string.IsNullOrEmpty(appUser.ProfileImageUrl))
                {
                    try
                    {
                        await _photoService.DeletePhotoAsync(appUser.ProfileImageUrl);
                    }
                    catch (Exception ex)
                    {
                       
                    }
                }

                appUser.ProfileImageUrl = photoResult.Url.ToString();

                await _userManager.UpdateAsync(appUser);

                return Ok(appUser.ToUserDto());
            }

            return Ok(appUser.ToUserDto());
        }
    }
}
