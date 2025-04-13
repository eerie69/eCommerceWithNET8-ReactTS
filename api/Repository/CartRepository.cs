using api.Dtos.Cart;
using api.Dtos.Order;
using api.Mappers;
using DemoShop.Data;
using DemoShop.Dtos.Cart;
using DemoShop.Interfaces;
using DemoShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace DemoShop.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _contex;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CartRepository> _logger;
        private readonly UserManager<AppUser> _userManager;

        public CartRepository(ApplicationDbContext contex, IHttpContextAccessor httpContextAccessor,
            ILogger<CartRepository> logger, UserManager<AppUser> userManager)
        {
            _contex = contex;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<int> AddItem(int productId, int qty)
        {
            string userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("user is not logged-in");

            using var transaction = await _contex.Database.BeginTransactionAsync();
            try
            {
                var cart = await GetCart(userId);
                if (cart == null)
                {
                    cart = new ShoppingCart { UserId = userId };
                    _contex.ShoppingCarts.Add(cart);
                    await _contex.SaveChangesAsync();
                }

                // cart detail section
                var cartItem = await _contex.CartDetails
                    .FirstOrDefaultAsync(c => c.ShoppingCartId == cart.Id && c.ProductId == productId);

                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    var product = await _contex.Products.FindAsync(productId);
                    if (product == null)
                        throw new InvalidOperationException("Product not found");

                    cartItem = new CartDetail
                    {
                        ProductId = productId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty,
                        UnitPrice = product.Price
                    };
                    _contex.CartDetails.Add(cartItem);
                }
                await _contex.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Откат транзакции при ошибке
                Console.WriteLine($"Error in AddItem: {ex.Message}");
                throw;
            }

            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount.CartQty;
        }
        public async Task<int> ReduceOrRemoveItem(int productId)
        {
            string userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("user is not logged-in");
            try
            {
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    throw new InvalidOperationException("Invalid cart");
                }
                // cart detail section
                var cartItem = await _contex.CartDetails
                                      .FirstOrDefaultAsync(c => c.ShoppingCartId == cart.Id && c.ProductId == productId);
                if (cartItem is null)
                    throw new InvalidOperationException("Not items in cart");

                else if (cartItem.Quantity == 1)
                    _contex.CartDetails.Remove(cartItem);
                else
                    cartItem.Quantity -= 1;

                await _contex.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ReduceOrRemoveItem: {ex.Message}");
                throw;
            }

            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount.CartQty;
        }

        public async Task<int> RemoveItem(int productId)
        {
            string userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("user is not logged-in");
            try
            {
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    throw new InvalidOperationException("Invalid cart");
                }
                // cart detail section
                var cartItem = await _contex.CartDetails
                                      .FirstOrDefaultAsync(c => c.ShoppingCartId == cart.Id && c.ProductId == productId);
                if (cartItem is null)
                    throw new InvalidOperationException("Not items in cart");
                else
                     _contex.CartDetails.Remove(cartItem);

               await _contex.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RemoveItem: {ex.Message}");
                throw;
            }

            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount.CartQty;
        }
        public async Task<ShoppingCart?> GetUserCart()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException("Invalid userId");

            return await _contex.ShoppingCarts
                .Include(c => c.CartDetails)
                    .ThenInclude(cd => cd.Product)
                        .ThenInclude(p => p.Stock)
                .Include(c => c.CartDetails)
                    .ThenInclude(cd => cd.Product)
                        .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<ShoppingCart?> GetCart(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("UserId cannot be null or empty", nameof(userId));

            return await _contex.ShoppingCarts
                        .Include(c => c.CartDetails)
                        .FirstOrDefaultAsync(x => x.UserId == userId && !x.IsDeleted);
        }
        public async Task<CartQuantityDto> GetCartItemCount(string userId = "")
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                        userId = GetUserId();

                if (string.IsNullOrEmpty(userId))
                    throw new InvalidOperationException("Invalid userId");

                var cart = await _contex.ShoppingCarts
                                        .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsDeleted);

                if (cart == null)
                    return new CartQuantityDto { CartQty = 0, CartTotal = 0 };

                var cartDetails = await _contex.CartDetails
                                       .Where(cd => cd.ShoppingCartId == cart.Id)
                                       .ToListAsync();

                var totalQuantity = cartDetails.Sum(cd => cd.Quantity);
                var totalAmount = cartDetails.Sum(cd => cd.Quantity * cd.UnitPrice);

                return new CartQuantityDto
                {
                    CartQty = totalQuantity,
                    CartTotal = totalAmount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в методе GetCartItemCount для userId: {UserId}", GetUserId());
                return new CartQuantityDto { CartQty = 0, CartTotal = 0 };
            }
        }

        public async Task<bool> DoCheckout(CheckoutDto modelDto)
        {
            using var transaction = await _contex.Database.BeginTransactionAsync();

            try
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new UnauthorizedAccessException("User is not logged-in");

                var cart = await GetCart(userId);
                if (cart is null)
                    throw new InvalidOperationException("Invalid cart");

                var cartDetail = cart.CartDetails;
                if (cartDetail == null || cartDetail.Count == 0)
                    throw new InvalidOperationException("Cart is empty");

                var pendingRecord = await _contex.OrderStatuses.FirstOrDefaultAsync(c => c.StatusName == "Pending");
                if (pendingRecord is null)
                    throw new InvalidOperationException("Order status does not have Pending status");

                var productIds = cart.CartDetails.Select(x => x.ProductId).ToList();
                var stocks = await _contex.Stocks
                                          .Where(s => productIds.Contains(s.ProductId))
                                          .ToListAsync();

                var totalPrice = CalculateCartTotal(cartDetail);

                var order = new Order
                {
                    UserId = userId,
                    CreateAt = DateTime.UtcNow,
                    PaymentMethod = modelDto.PaymentMethod,
                    isPaid = false,
                    TotalPrice = totalPrice,
                    OrderStatusId = pendingRecord.Id
                };
                _contex.Add(order);
                await _contex.SaveChangesAsync();

                foreach (var item in cartDetail)
                {
                    var stock = stocks.FirstOrDefault(s => s.ProductId == item.ProductId);
                    if (stock == null)
                        throw new InvalidOperationException("Stock is null");

                    if (item.Quantity > stock.Quantity)
                        throw new InvalidOperationException($"Only {stock.Quantity} items(s) are available in the stock");

                    var dto = new CreateOrderDetailDto
                    {
                        ProductId = item.ProductId,
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };

                    var orderDetail = dto.ToOrderDetailFromCreateDTO();
                    _contex.OrderDetails.Add(orderDetail);

                    // Уменьшаем количество товаров на складе
                    stock.Quantity -= item.Quantity;
                }

                // Удаление товаров из корзины
                _contex.CartDetails.RemoveRange(cartDetail);
                await _contex.SaveChangesAsync();
                await transaction.CommitAsync(); // Фиксируем транзакцию
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Откатываем транзакцию в случае ошибки
                _logger.LogError($"Checkout error: {ex.Message}");
                return false;
            }
        }

        private decimal CalculateCartTotal(List<CartDetail> cartDetails)
        {
            if (cartDetails == null || cartDetails.Count == 0) return 0;

            return cartDetails.Sum(cd => cd.Quantity * cd.UnitPrice);
        }

        private string GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated");

            string userId = _userManager.GetUserId(user);
            if(userId == null)
                throw new UnauthorizedAccessException("User ID not found");

            return userId;
        }
    }
}
