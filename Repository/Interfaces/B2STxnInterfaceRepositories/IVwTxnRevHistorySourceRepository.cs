using B2STxn.Models;
using Data.Base;
using Dto.B2STxn;
using Repository.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.B2STxnInterfaceRepositories
{
    public interface IVwTxnRevHistorySourceRepository : IRepository<VwTxnRevHistorySource>
    {
        Query GetBySwitchCode(int SwitchCode, DateTime ToTime, DateTime FromTime, int count);
        Task<VwTxnRevHistorySource> GetLastBySwitchCode(int SwitchCode, DateTime StartTime,CancellationToken cancellationToken);
        Task<List<TxnBriefBySwitchCode>> GetTxnHisBySwitchCode(int count, List<int> SwitchCodes, CancellationToken cancellationToken);
        Task<List<TxnBriefBySwitchCode>> GetLastItem(List<int> SwitchCodes, CancellationToken cancellationToken);
    }
}
