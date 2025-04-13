using DemoShop.Dtos.Stock;
using DemoShop.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoShop.Controllers
{
    [Route("api/stock")]
    [ApiController]
    //[Authorize(Roles = nameof(Roles.Admin))]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;

        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string sTerm = "")
        {
            var stocks = await _stockRepo.GetStocks(sTerm);
            return Ok(stocks);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> ManageStock([FromRoute] int productId)
        {
            var existingStock = await _stockRepo.GetStockByProductId(productId);
            var stock = new StockDto
            {
                ProductId = productId,
                Quantity = existingStock != null ? existingStock.Quantity : 0,
            };

            return Ok(stock);
        }

        [HttpPut]
        public async Task<IActionResult> ManageStock(StockDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _stockRepo.ManageStock(stockDto);
                //TempData["successMessage"] = "Stock is updated successfully";
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            return Ok();
        }
    }
}
