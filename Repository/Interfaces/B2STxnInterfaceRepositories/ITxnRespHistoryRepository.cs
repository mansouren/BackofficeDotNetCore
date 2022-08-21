using B2STxn.Models;
using Data.B2STxn.Other;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2STxnInterfaceRepositories
{
    public interface ITxnRespHistoryRepository : IRepository<TxnRespHistory>
    {
        DateTime GetMax(string fieldname);
        Task<KeyValuePair<long[], int[]>> GetTxnHisByResp(int count, TransactionType transactionType, DateTime finishTime, List<string> failresps);
        DateTime GetServerTime();
        KeyValuePair<long[], int[]> AddtxnHisByResp(List<string> failresps);

    }
}
