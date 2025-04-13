using api.Dtos.Category;
using api.Mappers;
using DemoShop.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoShop.Controllers
{
    [Route("api/Category")]
    [ApiController]
    //[Authorize(Roles = nameof(Roles.Admin))]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryRepo.GetAllAsync();
            var categoryDto = category.Select(s => s.ToCategoryDto());
            return Ok(categoryDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category.ToCategoryDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto categoryDto)
        {
            if(!ModelState.IsValid)
               return BadRequest(ModelState);

            try
            {
                var categoryModel = categoryDto.ToCategoryFromCreate();
                await _categoryRepo.CreateAsync(categoryModel);
                return CreatedAtAction(nameof(GetById), new { id = categoryModel.Id }, categoryModel.ToCategoryDto());
            }
            catch (Exception ex)
            {
                return BadRequest("Category could not added!");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var category = await _categoryRepo.UpdateAsync(id, categoryDto.ToCategoryFromUpdate());
                if (category == null) return NotFound("Category not found");

                return Ok(category.ToCategoryDto());
            }
            catch (Exception ex)
            {
                return BadRequest("Category could not updated!");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryModel = await _categoryRepo.DeleteAsync(id);
            if (categoryModel == null) return NotFound("Category does not exist");

            return Ok(categoryModel);
        }
    }
}
