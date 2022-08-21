using B2STxn.Models;
using Data.B2STxn.Other;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Repository.Interfaces.B2STxnInterfaceRepositories;
using Utilities;

namespace Monitoring.Hubs
{
    public class MonitoringHub : Hub
    {
        private readonly ITxnRespHistoryRepository repository;

        public MonitoringHub(ITxnRespHistoryRepository repository)
        {
            this.repository = repository;
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

        public override async Task OnConnectedAsync()
        {

            try
            {
                var datasets = new List<dynamic>();

                DateTime FinishTime = DateTime.Now;

                try
                {
                    FinishTime = repository.GetMax(TxnRespHistory.Columns.EndTime);
                }
                catch
                {
                    try
                    {
                        FinishTime = repository.GetServerTime();
                    }
                    catch
                    {
                        FinishTime = DateTime.Now;
                    }
                }

                var yesterdayalltxn = await repository.GetTxnHisByResp(60, TransactionType.Yesterday, FinishTime, FailResps);
                var failtxn = await repository.GetTxnHisByResp(60, TransactionType.fail, FinishTime, FailResps);
                var alltxn = await repository.GetTxnHisByResp(60, TransactionType.All, FinishTime, FailResps);
                var suctxn = await repository.GetTxnHisByResp(60, TransactionType.successful, FinishTime, FailResps);


                datasets.Add(new
                {
                    data = suctxn.Value,
                    label = "موفق",
                    Key = "Success",
                    color = "#308014"
                });

                datasets.Add(new
                {
                    data = failtxn.Value,
                    label = "نا موفق",
                    Key = "Fail",
                    color = "#ff0000"
                });

                datasets.Add(new
                {
                    data = alltxn.Value,
                    label = "کل",
                    Key = "Total",
                    color = "#7094db"
                });

                datasets.Add(new
                {
                    data = yesterdayalltxn.Value,
                    label = "کل دیروز",
                    Key = "Yesterday",
                    color = "#b3b3b3"
                });
                var result = new
                {
                    success = true,
                    labels = alltxn.Key,
                    datasets = datasets
                };

                await Clients.All.SendAsync("populatetxns", JsonConvert.SerializeObject(result));
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

    }
}


