using DemoShop.Data;
using DemoShop.Dtos.Order;
using DemoShop.Interfaces;
using DemoShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DemoShop.Repository
{
    public class UserOrderRepository : IUserOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public UserOrderRepository(ApplicationDbContext context, 
            IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task ChangeOrderStatus(UpdateOrderStatusDto data)
        {
            var order = await _context.Orders.FindAsync(data.OrderId);
            if (order == null)
            {
                throw new InvalidOperationException($"order within id: {data.OrderId} does not found");
            }
            
            order.OrderStatusId = data.OrderStatusId;
            await _context.SaveChangesAsync();
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<List<OrderStatus>> GetOrderStatuses()
        {
            return await _context.OrderStatuses.ToListAsync();
        }

        public async Task TogglePaymentStatus(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new InvalidOperationException($"Order within id: {orderId} does not found");
            }

            order.isPaid = !order.isPaid;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> UserOrders(bool getAll = false)
        {
            var orders = _context.Orders
                                 .Include(x => x.OrderStatus)
                                 .Include(x => x.OrderDetail)
                                 .ThenInclude(x => x.Product)
                                 .ThenInclude(x => x.Category)
                                 .AsQueryable();
            if(!getAll)
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged-in");

                orders = orders.Where(a => a.UserId == userId);
                return await orders.ToListAsync();
            }

            return await orders.ToListAsync();
        }

        private string GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            string userId = _userManager.GetUserId(user);
            if (userId == null)
                throw new UnauthorizedAccessException("User ID not found");

            return userId;
        }
    }
}
