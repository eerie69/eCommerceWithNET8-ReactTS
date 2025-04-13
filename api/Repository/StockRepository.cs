using DemoShop.Data;
using DemoShop.Dtos.Stock;
using DemoShop.Interfaces;
using DemoShop.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoShop.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetStockByProductId(int productId)
        {
            return await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task ManageStock(StockDto stockToManage)
        {
            // if there is no stock for given book id, then add new record
            // if there is already stock for given book id, update stock's quantity
            var existingStock = await GetStockByProductId(stockToManage.ProductId);
            if (existingStock is null)
            {
                var stock = new Stock
                {
                    ProductId = stockToManage.ProductId,
                    Quantity = stockToManage.Quantity,
                };

                _context.Stocks.Add(stock);
            }
            else
            {
                existingStock.Quantity = stockToManage.Quantity;
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockDisplayDto>> GetStocks(string sTerm = "")
        {
            var stocks = await (from product in _context.Products
                                join stock in _context.Stocks
                                on product.Id equals stock.ProductId
                                into product_stock
                                from productStock in product_stock.DefaultIfEmpty()
                                where string.IsNullOrWhiteSpace(sTerm) || 
                                product.Title.ToLower().Contains(sTerm.ToLower())
                                select new StockDisplayDto
                                {
                                    ProductId = product.Id,
                                    ProductName = product.Title,
                                    Quantity = productStock.Quantity == null ? 0 : productStock.Quantity,
                                }).ToListAsync();

            return stocks;
        }
    }
}
