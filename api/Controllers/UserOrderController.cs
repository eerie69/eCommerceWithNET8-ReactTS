using api.Mappers;
using DemoShop.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoShop.Controllers
{
    [Route("api/UserOrders")]
    [ApiController]
    [Authorize]
    public class UserOrderController : ControllerBase
    {
        private readonly IUserOrderRepository _userOrderRepo;
        public UserOrderController(IUserOrderRepository userOrderRepo)
        {
            _userOrderRepo = userOrderRepo;
        }

        [HttpGet]
        public async Task<IActionResult> UserOrders()
        {
            var orders = await _userOrderRepo.UserOrders();
            if (orders == null) return BadRequest("Orders not found");

            var ordersDto = orders.Select(c => c.ToOrderDto()).ToList();
            return Ok(ordersDto);
        }
    }
}
