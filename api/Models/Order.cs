using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoShop.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }= string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [Required]
        public int OrderStatusId { get; set; }
        public bool IsDeleted { get; set; } = false;
        [Required] 
        public string PaymentMethod { get; set; } = string.Empty; 
        public bool isPaid { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; } = null!;
        public List<OrderDetail> OrderDetail { get; set; } = null!;
    }
}
