using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoShop.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        [Required] 
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; } // Quantity * UnitPrice
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
