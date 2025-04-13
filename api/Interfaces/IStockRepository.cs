using DemoShop.Dtos.Stock;
using DemoShop.Models;

namespace DemoShop.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock?> GetStockByProductId(int productId);
        Task ManageStock(StockDto stockToManage);
        Task<IEnumerable<StockDisplayDto>> GetStocks(string sTerm = "");
    }
}
