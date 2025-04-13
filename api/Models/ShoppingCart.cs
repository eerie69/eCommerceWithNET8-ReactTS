using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoShop.Models
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

        public AppUser AppUser { get; set; } = null!;
        public List<CartDetail> CartDetails { get; set; } = new List<CartDetail>();


        [NotMapped]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CartTotal => CartDetails.Sum(cd => cd.Quantity * cd.UnitPrice);
    }
}
