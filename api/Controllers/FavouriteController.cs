using DemoShop.Extensions;
using DemoShop.Interfaces;
using DemoShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoShop.Controllers
{
    [Route("Favourite")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepository _productRepo;
        private readonly IFavouriteRepository _favouriteRepo;

        public FavouriteController(UserManager<AppUser> userManager,
        IProductRepository productRepo, IFavouriteRepository favouriteRepo)
        {
            _userManager = userManager;
            _productRepo = productRepo;
            _favouriteRepo = favouriteRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var AppUser = await _userManager.FindByNameAsync(username);
            var userFavourite = await _favouriteRepo.GetUserFavourite(AppUser);
            return Ok(userFavourite);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddFavourite(string title)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var product = await _productRepo.GetByTitleAsync(title);

            if (product == null) return BadRequest("Product not found");

            var userPortfolio = await _favouriteRepo.GetUserFavourite(appUser);

            if (userPortfolio.Any(e => e.Title.ToLower() == title.ToLower())) return BadRequest("Cannot add same product to favourite");

            var favouriteModel = new Favourite
            {
                ProductId = product.Id,
                AppUserId = appUser.Id
            };

            await _favouriteRepo.CreateAsync(favouriteModel);

            if (favouriteModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string title)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var userFavourite = await _favouriteRepo.GetUserFavourite(appUser);

            var filteredProduct = userFavourite.Where(s => s.Title.ToLower() == title.ToLower()).ToList();

            if (filteredProduct.Count() == 1)
            {
                await _favouriteRepo.DeleteFavourite(appUser, title);
            }
            else
            {
                return BadRequest("Product not in your favourite");
            }

            return Ok();
        }
    }
}
