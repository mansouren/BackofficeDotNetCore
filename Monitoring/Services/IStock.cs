using Monitoring.Models;


namespace Monitoring.Services
{
    public interface IStock
    {
        Task<ProductModel> GetStockData(int productid);
        
    }
}
