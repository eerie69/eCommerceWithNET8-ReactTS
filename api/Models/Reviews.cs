using DemoShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Reviews")]
    public class Reviews
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; } = string.Empty; 
        [Required]
        [Range(0.5, 5)]
        public double Rating { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [Required]
        public int ProductId { get; set; } 
        public Product Product { get; set; }  = null!;
        [Required]
        public string AppUserId { get; set; } = string.Empty; 
        public AppUser AppUser { get; set; } = null!;
    }
}
