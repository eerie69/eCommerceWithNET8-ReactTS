using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Order
{
    public class CreateOrderDetailDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
    }
}
