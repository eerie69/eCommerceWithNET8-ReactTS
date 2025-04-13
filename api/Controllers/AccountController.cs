using DemoShop.Data;
using DemoShop.Dtos.Account;
using DemoShop.Interfaces;
using DemoShop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

namespace DemoShop.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;



        public AccountController(UserManager<AppUser> userManager, 
            ITokenService tokenService, SignInManager<AppUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _logger = logger;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isEmail = IsValidEmail(loginDto.Email); //Проверяем, является ли введённый логин настоящим Email
            var user = isEmail // Ищем пользователя одним вызовом (FindByEmailAsync) или (FindByNameAsync)
                ? await _userManager.FindByEmailAsync(loginDto.Email)
                : await _userManager.FindByNameAsync(loginDto.Email);

            if(user == null)
            return Unauthorized("Invalid username or password.");

            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);


            if (result.Succeeded)
            {
                return Ok(
                    new NewUserDto
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)
                    }
                );
            }

            if (result.IsLockedOut)
                return Unauthorized("User account locked out.");

            return Unauthorized("Invalid login attempt.");
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
                return BadRequest("Email is already in use.");

            var appUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (!createdUser.Succeeded)
                return BadRequest(createdUser.Errors);

            var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
            if (!roleResult.Succeeded)
                return StatusCode(500, roleResult.Errors);

            return Ok(new NewUserDto
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                Email = appUser.Email,
                Token = _tokenService.CreateToken(appUser)
            });
        }
    }
}
