using Microsoft.AspNetCore.SignalR;
using Monitoring.Hubs;
using System.Text.Json;

namespace Monitoring.Services
{
    public class StockServiceBackgroundCaller : BackgroundService
    {
        public IStock StockService { get; }
        public IHubContext<MonitoringHub> HubContext { get; }
        private int productid = 1;
        public StockServiceBackgroundCaller(IStock stockService, IHubContext<MonitoringHub> hubContext)
        {
            StockService = stockService;
            HubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var product = await StockService.GetStockData(productid);
                await HubContext.Clients.All.SendAsync("populateData", JsonSerializer.Serialize(product));
                Interlocked.Increment(ref productid);
                if (productid == 20)
                {
                    productid = 1;
                }
                if (productid > 1)
                {
                    await Task.Delay(2000);
                }
            }
        }
    }
}
