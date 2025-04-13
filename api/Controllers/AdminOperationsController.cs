using api.Dtos.Order;
using DemoShop.Constants;
using DemoShop.Dtos.Order;
using DemoShop.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoShop.Controllers
{
    [Route("api/AdminOperations")]
    [ApiController]
    //[Authorize(Roles = nameof(Roles.Admin))]
    public class AdminOperationsController : ControllerBase
    {
        private readonly IUserOrderRepository _userOrderRepo;

        public AdminOperationsController(IUserOrderRepository userOrderRepo)
        {
            _userOrderRepo = userOrderRepo;
        }

        [HttpGet]
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _userOrderRepo.UserOrders(true);
            return Ok(orders);
        }

        [HttpPost("toggle-payment")]
        public async Task<IActionResult> TogglePaymentStatus([FromBody] TogglePaymentStatusDto request)
        {
            if (request.OrderId <= 0)
                return BadRequest(new { error = "Invalid order ID" });

            try
            {
                await _userOrderRepo.TogglePaymentStatus(request.OrderId);
                return Ok(new { message = "Payment status updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusDto data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userOrderRepo.ChangeOrderStatus(data);
                return Ok(new { message = "Order status updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("statuses")]
        public async Task<IActionResult> GetOrderStatuses()
        {
            var statuses = await _userOrderRepo.GetOrderStatuses();
            return Ok(statuses);
        }
    }
}
