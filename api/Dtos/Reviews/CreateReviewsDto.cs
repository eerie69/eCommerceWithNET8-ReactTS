using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Reviews
{
    public class CreateReviewsDto
    {
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters")]
        public string Content { get; set; } = string.Empty;
        [Required]
        [Range(0.5, 5)]
        public double Rating { get; set; }

    }
}
