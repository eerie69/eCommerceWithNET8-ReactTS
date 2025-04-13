using api.Dtos.Reviews;

namespace api.Dtos.Cart
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public decimal CartTotal { get; set; }
        public List<CartDetailDto> CartDetails { get; set; } = new List<CartDetailDto>();
    }
}
