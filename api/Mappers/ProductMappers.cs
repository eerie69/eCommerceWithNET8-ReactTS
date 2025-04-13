using api.Mappers;
using DemoShop.Dtos.Product;
using DemoShop.Interfaces;
using DemoShop.Models;

namespace DemoShop.Mappers
{
    public static class ProductMappers
    {

        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto
            {
                Id = productModel.Id,
                Title = productModel.Title,
                Description = productModel.Description,
                Price = productModel.Price,
                Image = productModel.Image,
                Featured = productModel.Featured,
                CategoryName = productModel.Category?.CategoryName ?? "Unknown",
                DateUploaded = productModel.DateUploaded,
                Quantity = productModel.Stock?.Quantity ?? 0,
                CartQty = productModel.CartDetail?.Sum(s => s.Quantity) ?? 0,
                Reviews = productModel.Reviews?.Select(c => c.ToReviewsDto()).ToList()
            };
        }

        public static Product ToProductFromCreateDTO(this CreateProductRequestDto productDto)
        {   

            return new Product
            {
                Title = productDto.Title,
                Description = productDto.Description,
                Price = productDto.Price,
                Featured = productDto.Featured,
                CategoryId = productDto.CategoryId,
            };
        }
    }
}
