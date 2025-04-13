using api.Dtos.Cart;
using api.Mappers;
using DemoShop.Dtos.Cart;
using DemoShop.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepo;
        public CartController(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int productId, int qty = 1, int redirect = 0)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (productId <= 0 || qty <= 0)
                return BadRequest("Invalid product ID or quantity.");

            var cartCount = await _cartRepo.AddItem(productId, qty);

            return redirect == 0 ? Ok(cartCount) : Created();
        }

        [HttpDelete("reduceOrRemove-item")]
        public async Task<IActionResult> ReduceOrRemoveItem(int productId)
        {
            var cartCount = await _cartRepo.ReduceOrRemoveItem(productId);
            return NoContent();
        }

        [HttpDelete("remove-item")]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            var cartCount = await _cartRepo.RemoveItem(productId);
            return NoContent();
        }

        [HttpGet("user-cart")]
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            return cart != null ? Ok(cart.ToShoppingCartDto()) : NotFound("Cart not found.");
        }

        [HttpGet("total-items")]
        public async Task<IActionResult> GetTotalItemInCart()
        {
            var cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutDto modelDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isCheckedOut = await _cartRepo.DoCheckout(modelDto);

            return isCheckedOut ? Ok("Checkout successful.") : BadRequest("Checkout failed.");
        }
    }
}
