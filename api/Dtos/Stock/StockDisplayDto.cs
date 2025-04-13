namespace DemoShop.Dtos.Stock
{
    public class StockDisplayDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? ProductName { get; set; }
    }
}
