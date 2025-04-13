using api.Dtos.Cart;
using api.Dtos.Order;
using DemoShop.Dtos.Order;
using DemoShop.Dtos.Product;
using DemoShop.Models;

namespace api.Mappers
{
    public static class OrderMappers
    {
        public static OrderDto ToOrderDto(this Order orderModel)
        {
            return new OrderDto
            {
                Id = orderModel.Id,
                UserId = orderModel.UserId,
                CreatedAt = DateTime.Now,
                Status = orderModel.OrderStatus?.StatusName ?? "Unknown",
                PaymentMethod = orderModel.PaymentMethod,
                TotalPrice = orderModel.TotalPrice,
                Details = orderModel.OrderDetail.Select(s => s.ToOrderDetailDto()).ToList(),
            };
        }

        public static OrderDetailDto ToOrderDetailDto(this OrderDetail orderModel)
        {
            return new OrderDetailDto
            {
                Id = orderModel.Id,
                OrderId = orderModel.OrderId,
                ProductId = orderModel.ProductId,
                Quantity = orderModel.Quantity,
                UnitPrice = orderModel.UnitPrice
            };
        }

        public static OrderDetail ToOrderDetailFromCreateDTO(this CreateOrderDetailDto orderDto)
        {

            return new OrderDetail
            {
                ProductId = orderDto.ProductId,
                OrderId = orderDto.OrderId,
                Quantity = orderDto.Quantity,
                UnitPrice = orderDto.UnitPrice,
            };
        }
    }
}
