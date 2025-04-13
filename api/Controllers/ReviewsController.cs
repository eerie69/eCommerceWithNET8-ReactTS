using api.Dtos.Reviews;
using api.Interfaces;
using api.Mappers;
using DemoShop.Extensions;
using DemoShop.Helpers;
using DemoShop.Interfaces;
using DemoShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("Reviews")]
    [ApiController]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsRepository _reviewsRepo;
        private readonly IProductRepository _productRepo;
        private readonly UserManager<AppUser> _userManager;

        public ReviewsController(IReviewsRepository reviewsRepo,
        IProductRepository productRepo, UserManager<AppUser> userManager)
        {
            _reviewsRepo = reviewsRepo;
            _productRepo = productRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject queryObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviews = await _reviewsRepo.GetAllAsync(queryObject);
            if (reviews == null) return NotFound("Reviews not found");
            var reviewsDto = reviews.Select(s => s.ToReviewsDto());

            return Ok(reviewsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _reviewsRepo.GetByIdAsync(id);
            if (comment == null) return NotFound("Comment not found");

            return Ok(comment.ToReviewsDto());
        }

        [HttpPost]
        //[Route("{{title:regex(^[a-zA-Z0-9 ]*$)}}")]
        [Route("{title}")]
        public async Task<IActionResult> Create([FromRoute] string title, CreateReviewsDto reviewsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepo.GetByTitleAsync(title);
            if (product == null) return BadRequest("Product does not exists");

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) return BadRequest($"{username} not found.");

            var reviewsModel = reviewsDto.ToReviewsFromCreate(product.Id);
            reviewsModel.AppUserId = appUser.Id;

            await _reviewsRepo.CreateAsync(reviewsModel);
            return CreatedAtAction(nameof(GetById), new { id = reviewsModel.Id }, reviewsModel.ToReviewsDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReviewsRequestDto UpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) return BadRequest($"{username} not found.");

            var reviews = await _reviewsRepo.UpdateAsync(id, UpdateDto.ToReviewsFromUpdate(id));
            reviews.AppUserId = appUser.Id;
            if (reviews == null) return NotFound("Reviews not found");

            return Ok(reviews.ToReviewsDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewsModel = await _reviewsRepo.DeleteAsync(id);
            if (reviewsModel == null) return NotFound("Comment does not exist");

            return Ok(reviewsModel);
        }
    }
}
