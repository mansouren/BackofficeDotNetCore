using B2STxn.Models;
using Data;
using Data.Interfaces;
using Repository.Interfaces.B2STxnInterfaceRepositories;
using Repository.PublicClasses;

using PetaPoco;
using Data.B2STxn.Other;
using Data.Base;
using Utilities;

namespace Repository.Repositories.B2STxnRepositories
{
    public class TxnRespHistoryRepository : B2STxnRepository<TxnRespHistory>, ITxnRespHistoryRepository
    {
        public TxnRespHistoryRepository(IB2STxnDBContext dbContext) : base(dbContext)
        {

        }

        public KeyValuePair<long[], int[]> AddtxnHisByResp(List<string> failresps)
        {
            try
            {

                var values = new List<TxnBrief>();

                DateTime CurrentTime = DateTime.Now;

                try
                {
                    CurrentTime = GetMax(TxnRespHistory.Columns.StartTime);
                }
                catch
                {
                    try
                    {
                        CurrentTime = GetServerTime();
                    }
                    catch
                    {
                        CurrentTime = DateTime.Now;
                    }
                }


                CurrentTime = CurrentTime.AddTicks(-(CurrentTime.Ticks % TimeSpan.TicksPerSecond));
                var yesterdaystarttime = CurrentTime.AddDays(-1);


                try
                {
                    var query = new Query();
                    query.Sum(TxnRespHistory.Schema, TxnRespHistory.Columns.Count, true);
                    query.AND(TxnRespHistory.Columns.StartTime, CurrentTime);
                    query.AND(TxnRespHistory.Columns.Resp, BComparison.NotIn, failresps);

                    // موفق
                    values.Add(new TxnBrief()
                    {
                        StartTime = CurrentTime,
                        Value = DBContext.Database.FirstOrDefault<int>(query.q)
                    });
                }
                catch
                {
                    values.Add(new TxnBrief() { StartTime = CurrentTime, Value = 0 });
                }

                try
                {
                    var query = new Query();
                    query.Sum(TxnRespHistory.Schema, TxnRespHistory.Columns.Count, true);
                    query.AND(TxnRespHistory.Columns.StartTime, CurrentTime);
                    query.AND(TxnRespHistory.Columns.Resp, BComparison.In, failresps);

                    // ناموفق
                    values.Add(new TxnBrief()
                    {
                        StartTime = CurrentTime,
                        Value = DBContext.Database.Fetch<int>(query.q).First()
                    });
                }
                catch
                {
                    values.Add(new TxnBrief() { StartTime = CurrentTime, Value = 0 });
                }

                try
                {
                    var query = new Query();
                    query.Sum(TxnRespHistory.Schema, TxnRespHistory.Columns.Count, true);
                    query.AND(TxnRespHistory.Columns.StartTime, CurrentTime);

                    // کل
                    values.Add(new TxnBrief()
                    {
                        StartTime = CurrentTime,
                        Value = DBContext.Database.Fetch<int>(query.q).First()
                    });
                }
                catch
                {
                    values.Add(new TxnBrief() { StartTime = CurrentTime, Value = 0 });
                }

                try
                {
                    var query = new Query();
                    query.Sum(TxnRespHistory.Schema, TxnRespHistory.Columns.Count, true);
                    query.AND(TxnRespHistory.Columns.StartTime, yesterdaystarttime);
                    // دیروز
                    values.Add(new TxnBrief()
                    {
                        StartTime = CurrentTime,
                        Value = DBContext.Database.Fetch<int>(query.q).First()

                    });
                }
                catch
                {
                    values.Add(new TxnBrief() { StartTime = yesterdaystarttime, Value = 0 });
                }

                //values.Reverse();

                var valuesData = values.Select(r => r.Value).ToList();
                var keysData = values.Select(r => r.StartTime.ToRoundSecond().ToTimeStamp()).First();

                int[] v = new int[valuesData.Count];
                for (int i = 0; i < valuesData.Count; i++)
                {
                    v[i] = valuesData[i];
                }

                return new KeyValuePair<long[], int[]>(new long[] { keysData }, v);
            }
            catch
            {
                throw;
            }
        }


        public DateTime GetMax(string fieldname)
        {
            try
            {
                var query = new Query();
                query.Max(TxnRespHistory.Schema, fieldname, true);
                var maxDateTime = DBContext.Database.ExecuteScalar<DateTime>(query.q);
                return maxDateTime;
                //return DBContext.Database.FirstOrDefault<DateTime>(query.q);
                //DBContext.Database.Fetch<DateTime>(query.q).FirstOrDefault();

            }
            catch
            {

                throw;
            }
        }

        public DateTime GetServerTime()
        {
            //var q = new Query();
            //q.Select("GETDATE()");
            return DBContext.Database.Fetch<DateTime>("GETDATE()").FirstOrDefault();

        }

        public async Task<KeyValuePair<long[], int[]>> GetTxnHisByResp(int count, TransactionType transactionType, DateTime finishTime, List<string> failresps)
        {
            try
            {
                var date = finishTime.AddMinutes(-count);
                var maxstartToday = date.AddMinutes(count);

                List<TxnBrief> values = null;
                var q = new Query();
                q.Select(TxnRespHistory.Columns.StartTime, "Sum(COUNT) as Value");
                q.From(TxnRespHistory.Schema);

                switch (transactionType)
                {
                    case TransactionType.successful:
                        q.AND(TxnRespHistory.Columns.StartTime, BComparison.GreaterOrEquals, date);
                        q.AND(TxnRespHistory.Columns.Resp, BComparison.NotIn, failresps);
                        q.AND(TxnRespHistory.Columns.StartTime, BComparison.LessOrEquals, maxstartToday);
                        break;

                    case TransactionType.fail:
                        q.AND(TxnRespHistory.Columns.StartTime, BComparison.GreaterOrEquals, date);
                        q.AND(TxnRespHistory.Columns.Resp, BComparison.In, failresps);
                        q.AND(TxnRespHistory.Columns.StartTime, BComparison.LessOrEquals, maxstartToday);
                        break;

                    case TransactionType.All:
                        q.AND(TxnRespHistory.Columns.StartTime, BComparison.GreaterOrEquals, date);
                        q.AND(TxnRespHistory.Columns.StartTime, BComparison.LessOrEquals, maxstartToday);
                        break;

                    case TransactionType.Yesterday:
                        var minstart = finishTime.AddDays(-1).AddMinutes(-count);
                        var maxstart = minstart.AddMinutes(count);

                        q.AND(TxnRespHistory.Columns.StartTime, BComparison.GreaterOrEquals, minstart);
                        q.AND(TxnRespHistory.Columns.StartTime, BComparison.LessThan, maxstart);
                        break;
                }

                q.GroupBy(TxnRespHistory.Columns.StartTime);
                q.ORDER_BY(TxnRespHistory.Columns.StartTime, "ASC");
                //values = new PetaPoco.Database(this.connectionstringname).Fetch<TxnBrief>(q.q);
                values = await DBContext.Database.FetchAsync<TxnBrief>(q.q);

                var valuesData = values.OrderBy(r => r.StartTime).Select(r => r.Value).ToList();
                var keysData = values.OrderBy(r => r.StartTime).Select(r => r.StartTime.ToRoundSecond().ToTimeStamp()).ToList();

                int[] v = new int[valuesData.Count];
                for (int i = 0; i < valuesData.Count; i++)
                    v[i] = valuesData[i];

                var k = new long[keysData.Count];
                for (int i = 0; i < keysData.Count; i++)
                    k[i] = keysData[i];

                return new KeyValuePair<long[], int[]>(k, v);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
