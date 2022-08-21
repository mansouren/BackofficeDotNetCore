
using Monitoring.Models;
using System.Text.Json;

namespace Monitoring.Services
{
    public class Stock : IStock
    {
        public IHttpClientFactory ClientFactory { get; }
        public Stock(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        public async Task<ProductModel> GetStockData(int productid)
        {
            var client = ClientFactory.CreateClient();
            var url = $"https://dummyjson.com/products/{productid}";
            var result = await client.GetAsync(url);
            result.EnsureSuccessStatusCode();
            var data = await result.Content.ReadAsStringAsync();
            var product = JsonSerializer.Deserialize<ProductModel>(data);
            return product;
        }
    }
}
