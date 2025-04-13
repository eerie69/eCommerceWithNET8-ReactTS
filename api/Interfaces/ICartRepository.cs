using api.Dtos.Cart;
using DemoShop.Dtos.Cart;
using DemoShop.Models;

namespace DemoShop.Interfaces
{
    public interface ICartRepository
    {
        Task<int> AddItem(int productId, int qty);
        Task<int> ReduceOrRemoveItem(int productId);
        Task<int> RemoveItem(int productId);
        Task<ShoppingCart?> GetUserCart();
        Task<ShoppingCart?> GetCart(string userId);
        Task<CartQuantityDto> GetCartItemCount(string userId = "");
        Task<bool> DoCheckout(CheckoutDto modelDto);
    }
}
