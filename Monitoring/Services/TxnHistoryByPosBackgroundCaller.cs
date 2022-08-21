using Microsoft.AspNetCore.SignalR;
using Monitoring.Hubs;
using Newtonsoft.Json;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.Interfaces.B2SMainInterfaceRepositories;
using Repository.Interfaces.B2STxnInterfaceRepositories;

namespace Monitoring.Services
{
    public class TxnHistoryByPosBackgroundCaller : BackgroundService
    {
        private readonly ITxnByPosConditionRepository txnByPosConditionRepository;
        private readonly IBasicValueRepository basicValueRepository;
        private readonly IConfigurationRepository configurationRepository;
        private readonly IHubContext<MonitoringHub> hubContext;

        public TxnHistoryByPosBackgroundCaller(ITxnByPosConditionRepository txnByPosConditionRepository, IBasicValueRepository basicValueRepository, IConfigurationRepository configurationRepository, IHubContext<MonitoringHub> hubContext)
        {
            this.txnByPosConditionRepository = txnByPosConditionRepository;
            this.basicValueRepository = basicValueRepository;
            this.configurationRepository = configurationRepository;
            this.hubContext = hubContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var terminalTypes = new List<int>()
                {
                    5,2,37
                };

                        var datasets = new List<dynamic>();
                        var posConditionList = await basicValueRepository.GetByBasicTypeID((int)Utilities.Enums.BasicType.TerminalTypeCode);
                        posConditionList.Join(terminalTypes, o => o.Identifier, i => i, (o, i) => o).OrderByDescending(x => x.SortOrder).ToList();
                        if (await configurationRepository.IsShaparakCheck())
                        {
                            posConditionList.RemoveAll(r => r.Identifier == (int)Utilities.Enums.BasicValue.TerminalTypeCode.KIOSK);
                            posConditionList.RemoveAll(r => r.Identifier == (int)Utilities.Enums.BasicValue.TerminalTypeCode.IVR);
                        }
                        var txns = await txnByPosConditionRepository.GetTxnHistoryByPosCondition(30, posConditionList);

                        var starttimes = txns.Select(r => r.StartTime).Distinct().ToList();

                        string[] labels = new string[starttimes.Count()];

                        for (int i = 0; i < starttimes.Count(); i++)
                            labels[i] = starttimes[i].TimeOfDay.Hours.ToString() + ":" + starttimes[i].TimeOfDay.Minutes.ToString().PadLeft(2, '0');


                        for (int c = 0; c < posConditionList.Count; c++)
                        {
                            var txn = txns.FindAll(r => r.Poscondition == posConditionList[c].Identifier);

                            int[] v = new int[txn.Count];
                            for (int i = 0; i < txn.Count; i++)
                            {
                                v[i] = txn[i].Value;
                            }


                            datasets.Add(new
                            {
                                data = v,
                                label = posConditionList[c].PersianTitle,
                                Key = posConditionList[c].Identifier
                            });

                        }

                        var result = new
                        {
                            labels = labels,
                            datasets = datasets,
                            success = true
                        };
                        await hubContext.Clients.All.SendAsync("gettxnhistorybypos", JsonConvert.SerializeObject(result));
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
