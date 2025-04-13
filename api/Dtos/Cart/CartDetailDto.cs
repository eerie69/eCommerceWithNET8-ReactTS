using DemoShop.Dtos.Product;

namespace api.Dtos.Cart
{
    public class CartDetailDto
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductDto Product { get; set; } = null!;

    }
}
