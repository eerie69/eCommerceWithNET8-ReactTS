using DemoShop.Dtos.Order;
using DemoShop.Models;

namespace DemoShop.Interfaces
{
    public interface IUserOrderRepository
    {
        Task<List<Order>> UserOrders(bool getAll = false);
        Task ChangeOrderStatus(UpdateOrderStatusDto data);
        Task TogglePaymentStatus(int orderId);
        Task<Order?> GetOrderById(int id);
        Task<List<OrderStatus>> GetOrderStatuses();
    }
}
