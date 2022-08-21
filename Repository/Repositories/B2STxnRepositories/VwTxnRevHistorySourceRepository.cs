using B2STxn.Models;
using Data;
using Data.Base;
using Data.Interfaces;
using Dto.B2STxn;
using Repository.Interfaces.B2STxnInterfaceRepositories;
using Repository.PublicClasses;

using Utilities;

namespace Repository.Repositories.B2STxnRepositories
{
    public class VwTxnRevHistorySourceRepository : B2STxnRepository<VwTxnRevHistorySource>, IVwTxnRevHistorySourceRepository
    {
        private readonly ITxnRespHistoryRepository txnRespHistoryRepository;

        public VwTxnRevHistorySourceRepository(IB2STxnDBContext dbContext, ITxnRespHistoryRepository txnRespHistoryRepository) : base(dbContext)
        {
            this.txnRespHistoryRepository = txnRespHistoryRepository;
        }

        public Query GetBySwitchCode(int SwitchCode, DateTime ToTime, DateTime FromTime, int count)
        {
            var q = base.GetAll(VwTxnRevHistorySource.Schema, count);
            q.AND(VwTxnRevHistorySource.Columns.SwitchCode, SwitchCode);
            q.AND(VwTxnRevHistorySource.Columns.StartTime, BComparison.GreaterOrEquals, FromTime);
            q.AND(VwTxnRevHistorySource.Columns.StartTime, BComparison.LessThan, ToTime);
            return q;
        }

        public async Task<VwTxnRevHistorySource> GetLastBySwitchCode(int SwitchCode, DateTime StartTime, CancellationToken cancellationToken)
        {
            var EndTime = StartTime.AddMinutes(1);
            var q = base.GetAll(nameof(VwTxnRevHistorySource), 1);
            q.AND(VwTxnRevHistorySource.Columns.SwitchCode, SwitchCode);
            q.AND(VwTxnRevHistorySource.Columns.StartTime, BComparison.GreaterOrEquals, StartTime);
            q.AND(VwTxnRevHistorySource.Columns.StartTime, BComparison.LessThan, EndTime);
            q.ORDER_BY(VwTxnRevHistorySource.Columns.ID, "DESC");
            return await DBContext.Database.FirstOrDefaultAsync<VwTxnRevHistorySource>(cancellationToken, q.q);

        }

        public async Task<List<TxnBriefBySwitchCode>> GetTxnHisBySwitchCode(int count, List<int> SwitchCodes, CancellationToken cancellationToken)
        {
            DateTime toTime = DateTime.Now;
            try
            {
                toTime = txnRespHistoryRepository.GetMax(TxnRespHistory.Columns.EndTime);
            }
            catch
            {

                try
                {
                    toTime = txnRespHistoryRepository.GetServerTime();
                }
                catch
                {

                    toTime = DateTime.Now;
                }
            }

            var FromTime = toTime.AddMinutes(-1 * count);

            var values = new List<TxnBriefBySwitchCode>();

            foreach (var item in SwitchCodes)
            {


                var query = GetBySwitchCode(item, toTime, FromTime, count);
                query.ORDER_BY(VwTxnRevHistorySource.Columns.StartTime, "ASC");
                var result = await DBContext.Database.FetchAsync<VwTxnRevHistorySource>(cancellationToken, query.q);

                for (var i = FromTime; i < toTime; i = i.AddMinutes(1))
                {
                    var v = result.Find(r => r.StartTime >= i && r.StartTime < i.AddMinutes(1));

                    values.Add(new TxnBriefBySwitchCode()
                    {
                        StartTime = i,
                        SwitchCode = item,
                        Count = v == null ? 0 : v.Count
                    });
                }


            }

            return values;
        }

        public async Task<List<TxnBriefBySwitchCode>> GetLastItem(List<int> SwitchCodes, CancellationToken cancellationToken)
        {
            var res = new List<TxnBriefBySwitchCode>();

            DateTime StartTime = DateTime.Now;

            try
            {
                StartTime = txnRespHistoryRepository.GetMax(TxnRespHistory.Columns.EndTime);
            }
            catch
            {
                try
                {
                    StartTime = txnRespHistoryRepository.GetServerTime();
                }
                catch
                {
                    StartTime = DateTime.Now;
                }
            }

            StartTime = StartTime.ToRoundSecond().AddMinutes(-1);

            foreach (var item in SwitchCodes)
            {
                var val = await GetLastBySwitchCode(item, StartTime, cancellationToken);
                res.Add(new TxnBriefBySwitchCode()
                {
                    StartTime = StartTime,
                    SwitchCode = item,
                    Count = val == null ? 0 : val.Count
                });
            }
            return res;
        }

    }
}
