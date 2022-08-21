using Microsoft.AspNetCore.SignalR;
using Monitoring.Hubs;
using Newtonsoft.Json;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.Interfaces.B2STxnInterfaceRepositories;

namespace Monitoring.Services
{
    public class TxnRevHistoryBackgroundCaller : BackgroundService
    {
        private readonly IHubContext<MonitoringHub> hubContext;
        private readonly IVwTxnRevHistorySourceRepository repository;
        private readonly ISingletoneSwitchRepositroy switchRepository;

        public TxnRevHistoryBackgroundCaller(IHubContext<MonitoringHub> hubContext, IVwTxnRevHistorySourceRepository repository, ISingletoneSwitchRepositroy switchRepository)
        {
            this.hubContext = hubContext;
            this.repository = repository;
            this.switchRepository = switchRepository;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           await Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var datasets = new List<dynamic>();

                        try
                        {

                            var SwitchCodes = new int[8] { 15, 18, 21, 23, 26, 29, 30, 31 }.ToList();

                            var Switches = await switchRepository.GetByIds(SwitchCodes, stoppingToken);

                            var values = await repository.GetTxnHisBySwitchCode(30, SwitchCodes, stoppingToken);

                            var StartTimes = values.Select(r => r.StartTime).Distinct().ToList();
                            string[] labels = new string[StartTimes.Count()];

                            for (int i = 0; i < StartTimes.Count(); i++)
                                labels[i] = StartTimes[i].TimeOfDay.Hours.ToString() + ":" + StartTimes[i].TimeOfDay.Minutes.ToString().PadLeft(2, '0');

                            datasets.Add(new
                            {
                                data = values.GroupBy(r => r.StartTime).Select(r => new { Count = r.Sum(x => x.Count) }).Select(r => r.Count).ToList(),
                                label = "کل",
                                key = -1
                            });

                            foreach (var item in SwitchCodes)
                            {
                                var txn = values.FindAll(r => r.SwitchCode == item);
                                var Switch = Switches.Find(r => r.SwitchCode == item);
                                datasets.Add(new
                                {
                                    data = txn != null ? txn.Select(r => r.Count).ToList() : new List<int>(),
                                    label = Switch == null ? "null" : Switch.SwitchName,
                                    key = item,

                                });
                            }

                            datasets = datasets.OrderByDescending(r => r.label).ToList();

                            var result = new
                            {
                                labels = labels,
                                datasets = datasets,
                                success = true
                            };

                            await hubContext.Clients.All.SendAsync("txnRevHis", JsonConvert.SerializeObject(result));
                            //Thread.Sleep(TimeSpan.FromMinutes(1));
                        }
                        catch (Exception ex)
                        {
                            //var v = new List<int>();
                            //var k = new List<string>();
                            //var d = DateTime.Now.AddDays(-1).AddMinutes(-30);
                            //for (int i = 0; i < 30; i++)
                            //{
                            //    k.Add(string.Format("{0}:{1}", d.TimeOfDay.Hours.ToString().PadLeft(2, '0'), d.TimeOfDay.Minutes.ToString().PadLeft(2, '0')));
                            //    v.Add(0);
                            //    d = d.AddMinutes(1);
                            //}

                            //datasets.Add(new
                            //{
                            //    Key = "Rev",
                            //    label = "MTI = 400",
                            //    data = v
                            //});

                            //var result = new
                            //{
                            //    success = true,
                            //    labels = k,
                            //    datasets = datasets
                            //};

                            // return (JsonConvert.SerializeObject(result));
                            throw new Exception(ex.Message);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //await Task.Delay(TimeSpan.FromSeconds(10));
                    }
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
            });
            
        }
    }
}
