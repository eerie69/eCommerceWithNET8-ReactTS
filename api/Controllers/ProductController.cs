using DemoShop.Dtos.Product;
using DemoShop.Helpers;
using DemoShop.Interfaces;
using DemoShop.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace DemoShop.Controllers
{
    [Route("api/Product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly IPhotoService _photoService;
        

        public ProductController(IProductRepository productRepo, IPhotoService photoService)
        {
            _productRepo = productRepo;
            _photoService = photoService;
            
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var products = await _productRepo.GetAllAsync(query);

            var productDto = products.Select(s => s.ToProductDto()).ToList();


            return Ok(productDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepo.GetByIdAsync(id);

            if(product == null)
            {
                return NotFound();
            }
            
            return Ok(product.ToProductDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductRequestDto productDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _photoService.AddPhotoAsync(productDto.Image);
            if (result == null) return NotFound("Photo upload failed");

            var productModel = productDto.ToProductFromCreateDTO();
            productModel.Image = result.Url.ToString();
            await _productRepo.CreateAsync(productModel);

            return CreatedAtAction(nameof(GetById), new { id = productModel.Id }, productModel.ToProductDto());
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromForm] UpdateProductRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit product");
                return BadRequest(ModelState);
            }
            var productModel = await _productRepo.UpdateAsync(id, updateDto);
            if (productModel == null) return NotFound();

            return Ok(productModel.ToProductDto());

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModel = await _productRepo.DeleteAsync(id);
            if (productModel == null) return NotFound();
            return NoContent();
        }
    }
}
