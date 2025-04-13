using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Category
{
    public class CreateCategoryDto
    {
        [MaxLength(20, ErrorMessage = "Category name cannot be over 20 characters")]
        public string CategoryName { get; set; } = string.Empty;
    }
}
