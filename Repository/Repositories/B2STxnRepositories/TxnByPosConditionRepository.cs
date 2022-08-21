using B2SMain.Models;
using B2STxn.Models;
using Data.B2STxn.Other;
using Data.Interfaces;
using Repository.Interfaces.B2SConfigInterfaceRepositories;
using Repository.Interfaces.B2SMainInterfaceRepositories;
using Repository.Interfaces.B2STxnInterfaceRepositories;
using Repository.PublicClasses;
using PetaPoco;

namespace Repository.Repositories.B2STxnRepositories
{
    public class TxnByPosConditionRepository : B2STxnRepository<TxnByPosCondition>, ITxnByPosConditionRepository
    {
       

        public TxnByPosConditionRepository(IB2STxnDBContext dbContext) : base(dbContext)
        {
          
        }

        private async Task<IEnumerable<TxnByPosCondition>> GetByPosCondition(int poscondition, DateTime starttime, int? count = null)
        {
            try
            {
                var txnByConditionList =(await DBContext.Database.FetchAsync<TxnByPosCondition>(x => x.PosCondition == poscondition && x.StartTime > starttime)).Take(Convert.ToInt32(count));
                return txnByConditionList;
            }
            catch
            {

                throw;
            }
        }

        public async Task<List<TxnBriefByPoscon>> GetTxnHistoryByPosCondition(int count, List<BasicValue> posconditions)
        {
            try
            {
                var date = DateTime.Now.AddDays(-2);
                var values = new List<TxnBriefByPoscon>();

                foreach (var item in posconditions)
                {
                    
                    var txnByPosConditions =await GetByPosCondition(item.Identifier, date, count);
                    var result =  txnByPosConditions.OrderByDescending(x => x.StartTime);
                    
                   
                    values.AddRange(result.Select(r => new TxnBriefByPoscon()
                    {
                        StartTime = r.StartTime,
                        Poscondition = item.Identifier,
                        Value = r.TxnCount
                    }).OrderByDescending(r => r.StartTime).Take(count).OrderBy(r => r.StartTime).ToList());
                }

                return values;

            }
            catch
            {

                throw;
            }
        }
    }
}
