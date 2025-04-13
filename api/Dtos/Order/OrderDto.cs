using DemoShop.Dtos.Order;

namespace api.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public List<OrderDetailDto> Details { get; set; } = new List<OrderDetailDto>();
    }
}
