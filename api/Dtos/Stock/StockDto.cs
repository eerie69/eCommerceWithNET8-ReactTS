using System.ComponentModel.DataAnnotations;

namespace DemoShop.Dtos.Stock
{
    public class StockDto
    {
        public int ProductId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int Quantity { get; set; }
    }
}
