using api.Dtos.Cart;
using DemoShop.Dtos.Product;
using DemoShop.Mappers;
using DemoShop.Models;

namespace api.Mappers
{
    public static class CartMappers
    {
        public static ShoppingCartDto ToShoppingCartDto(this ShoppingCart cartModel)
        {
            return new ShoppingCartDto
            {
                Id = cartModel.Id,
                UserId = cartModel.UserId,
                CartTotal = cartModel.CartTotal,
                CartDetails = cartModel.CartDetails.Select(s => s.ToCartDetailDto()).ToList(),
            };
        }

        public static CartDetailDto ToCartDetailDto(this CartDetail cartModel)
        {
            return new CartDetailDto
            {
                Id = cartModel.Id,
                ShoppingCartId = cartModel.ShoppingCartId,
                Quantity = cartModel.Quantity,
                UnitPrice = cartModel.UnitPrice,
                Product = cartModel.Product.ToProductDto()
                
            };
        }
    }
}
