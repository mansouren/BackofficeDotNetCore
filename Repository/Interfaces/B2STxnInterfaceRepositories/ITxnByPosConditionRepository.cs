using B2SMain.Models;
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
    public interface ITxnByPosConditionRepository : IRepository<TxnByPosCondition>
    {
        Task<List<TxnBriefByPoscon>> GetTxnHistoryByPosCondition(int count, List<BasicValue> posconditions);
    }
}
