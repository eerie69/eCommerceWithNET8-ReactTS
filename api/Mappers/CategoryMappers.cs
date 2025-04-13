using api.Dtos.Category;
using DemoShop.Dtos.Category;
using DemoShop.Mappers;
using DemoShop.Models;

namespace api.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDto ToCategoryDto(this Category categoryModel)
        {
            return new CategoryDto
            {
                Id = categoryModel.Id,
                CategoryName = categoryModel.CategoryName,
                Products = categoryModel.Products.Select(s => s.ToProductDto()).ToList()
                
            };
        }

        public static Category ToCategoryFromCreate(this CreateCategoryDto categoryDto)
        {
            return new Category
            {
                CategoryName = categoryDto.CategoryName
            };
        }

        public static Category ToCategoryFromUpdate(this UpdateCategoryDto categoryDto)
        {
            return new Category
            {
                CategoryName = categoryDto.CategoryName
            };
        }
    }
}
