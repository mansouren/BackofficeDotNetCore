using B2STxn.Models;
using Data.B2STxn.Other;
using Microsoft.AspNetCore.SignalR;
using Monitoring.Hubs;
using Newtonsoft.Json;
using Repository.Interfaces.B2STxnInterfaceRepositories;
using System.Text.Json;

namespace Monitoring.Services
{
    public class TxnRespHistoryBackgroundCaller : BackgroundService
    {

        public ITxnRespHistoryRepository Repository { get; }
        public IHubContext<MonitoringHub> HubContext { get; }
        //private bool isFirstCall = true;

        public TxnRespHistoryBackgroundCaller(ITxnRespHistoryRepository repository, IHubContext<MonitoringHub> hubContext)
        {
            Repository = repository;
            HubContext = hubContext;

        }
        private static List<string> FailResps
        {
            get
            {
                try
                {

                    return new List<string>() { "3", "92", "91", "96", "80", "84" };

                }
                catch
                {
                    return new List<string>();
                }
            }
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var newtxn = Repository.AddtxnHisByResp(FailResps);


                        var result = new
                        {
                            labels = newtxn.Key,
                            datasets = newtxn.Value
                        };

                        await HubContext.Clients.All.SendAsync("populateaddtxns", JsonConvert.SerializeObject(result));
                        Thread.Sleep(10000);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


            });
        }





    }
}
