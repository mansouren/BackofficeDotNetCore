using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Base
{
    [Serializable]
    public enum BComparison
    {
        Equals = 0,
        NotEquals = 1,
        Like = 2,
        NotLike = 3,
        GreaterThan = 4,
        GreaterOrEquals = 5,
        LessThan = 6,
        LessOrEquals = 7,
        Blank = 8,
        Is = 9,
        IsNot = 10,
        In = 11,
        NotIn = 12,
        OpenParentheses = 13,
        CloseParentheses = 14,
        BetweenAnd = 15
    }

    public class Query
    {
        public Sql q = null;
        String schem = null;
        private bool isNew = true;
        public Query()
        {
            q = new Sql();
            isNew = true;
        }
        public Query(string table, bool hasWithNolock, int? topcount = null)
        {
            if (topcount.HasValue)
                q = new Sql(string.Format("SELECT TOP {0} * FROM ", topcount.Value.ToString()) + table + " ");
            else
                q = new Sql("SELECT * FROM " + table + " ");

            if (hasWithNolock)
                q = q.Append(" with(nolock) ");
            schem = table;
            isNew = true;
        }



        public Query(string table, string[] columns, bool hasWithNolock = true)
        {
            q = new Sql(table, columns);

            if (hasWithNolock)
                q = q.Append(" with(nolock) ");

            schem = table;
            isNew = true;
        }


        public void Select(params object[] columns)
        {
            q = new Sql();

            var fileds = string.Empty;
            foreach (var item in columns)
            {
                fileds += item + ",";
            }
            fileds = fileds.Remove(fileds.Length - 1);
            q.Append("SELECT " + fileds);

        }
        public void From(string Value)
        {
            q.Append(" FROM " + Value);
        }
        public void WithNoLock()
        {
            q.Append(" WITH(NOLOCK) ");
        }
        public void Max(string table, string fieldname, bool hasWithNolock)
        {
            q = new Sql(string.Format("SELECT max({0}) FROM {1} ", fieldname, table + " "));

            if (hasWithNolock)
                q = q.Append(" with(nolock) ");
            schem = table;
            isNew = true;
        }

        public void Distinct(String table, string fieldname, bool hasWithNolock)
        {
            q = new Sql(string.Format("SELECT DISTINCT({0}) FROM {1} ", fieldname, table + " "));

            if (hasWithNolock)
                q = q.Append(" with(nolock) ");
            schem = table;
            isNew = true;
        }
        public void AND(string column, object Value)
        {
            if (isNew)
            {
                q.Where(column + " = @0", Value);
                isNew = false;
            }
            else
                q.Append("and " + column + " = @0", Value);
        }

        public void StartORWithAnd(String column, Object Value)
        {
            if (isNew)
            {
                q.Where("(" + column + " = @0 ", Value);
                isNew = false;
            }
            else
            {
                q.Append("and (" + column + " = @0 ", Value);
            }
        }

        public void StartOR(String column, Object Value)
        {
            if (isNew)
            {
                q.Where("(" + column + " = @0", Value);
                isNew = false;
            }
            else
            {
                q.Append("or (" + column + " = @0", Value);
            }
        }

        public void EndOR(String column, Object Value)
        {
            q.Append("or " + column + " = @0", Value);
            q.Append(")");
        }

        public void CloseParentheses()
        {
            q.Append(")");
        }

        public void OR(String column, Object Value)
        {
            q.Append("or " + column + " = @0", Value);
        }

        public void ORDER_BY(String column, String order)
        {
            q.OrderBy(column + " " + order);
        }

        public void ORDER_BYDescending(String column)
        {

            q.OrderBy($"{column} DESC");
        }

        public void ORDER_BY(String order, params object[] arg)
        {
            var c = string.Empty;
            foreach (var item in arg)
                c += item + ",";

            c = c.TrimEnd(',');

            q.OrderBy(c + " " + order);


        }

        public void AND(String column, BComparison comp, Object Value)
        {
            String cc = "";
            if (comp == BComparison.Equals)
                cc = "=";
            else if (comp == BComparison.GreaterOrEquals)
                cc = ">=";
            else if (comp == BComparison.GreaterThan)
                cc = ">";
            else if (comp == BComparison.In)
                cc = "in";
            else if (comp == BComparison.Is)
            {
                cc = "is NULL";
                Value = null;
            }
            else if (comp == BComparison.IsNot)
            {
                cc = "is not NULL";
                Value = null;
            }
            else if (comp == BComparison.LessOrEquals)
                cc = "<=";
            else if (comp == BComparison.LessThan)
                cc = "<";
            else if (comp == BComparison.Like)
                cc = "like";
            else if (comp == BComparison.NotEquals)
                cc = "<>";
            else if (comp == BComparison.NotIn)
                cc = "not in";
            else if (comp == BComparison.NotLike)
                cc = "not like";

            if (isNew)
            {
                if (Value != null)
                {
                    if (comp == BComparison.In || comp == BComparison.NotIn)
                        q.Where(column + " " + cc + " (@0)", Value);
                    else if (comp == BComparison.Like)
                        q.Where(column + " " + cc + " N'%" + Value.ToString() + "%'", null);
                    else
                        q.Where(column + " " + cc + " @0", Value);
                }
                else
                    q.Where(column + " " + cc);
                isNew = false;
            }
            else
            {
                q.Append(" and ");
                if (Value != null)
                {
                    if (comp == BComparison.In || comp == BComparison.NotIn)
                        q.Append(column + " " + cc + "(@0)", Value);
                    else if (comp == BComparison.Like)
                        q.Append(column + " " + cc + " N'%" + Value.ToString() + "%'", null);
                    else
                        q.Append(column + " " + cc + " @0", Value);
                }
                else
                    q.Append(column + " " + cc);
            }

        }

        public void OR(String column, BComparison comp, Object Value)
        {
            String cc = "";
            if (comp == BComparison.Equals)
                cc = "=";
            else if (comp == BComparison.GreaterOrEquals)
                cc = ">=";
            else if (comp == BComparison.GreaterThan)
                cc = ">";
            else if (comp == BComparison.In)
                cc = "in";
            else if (comp == BComparison.Is)
            {
                cc = "is NULL";
                Value = null;
            }
            else if (comp == BComparison.IsNot)
            {
                cc = "is not NULL";
                Value = null;
            }
            else if (comp == BComparison.LessOrEquals)
                cc = "<=";
            else if (comp == BComparison.LessThan)
                cc = "<";
            else if (comp == BComparison.Like)
                cc = "like";
            else if (comp == BComparison.NotEquals)
                cc = "<>";
            else if (comp == BComparison.NotIn)
                cc = "not in";
            else if (comp == BComparison.NotLike)
                cc = "not like";

            if (isNew)
            {
                if (Value != null)
                {
                    if (comp == BComparison.In || comp == BComparison.NotIn)
                        q.Where(column + " " + cc + " (@0)", Value);
                    else if (comp == BComparison.Like)
                        q.Where(column + " " + cc + " N'%" + Value.ToString() + "%'", null);
                    else
                        q.Where(column + " " + cc + " @0", Value);
                }
                else
                    q.Where(column + " " + cc);
                isNew = false;
            }
            else
            {
                q.Append(" or ");
                if (Value != null)
                {
                    if (comp == BComparison.In || comp == BComparison.NotIn)
                        q.Append(column + " " + cc + "(@0)", Value);
                    else if (comp == BComparison.Like)
                        q.Append(column + " " + cc + " N'%" + Value.ToString() + "%'", null);
                    else
                        q.Append(column + " " + cc + " @0", Value);
                }
                else
                    q.Append(column + " " + cc);
            }
        }

        public Sql ExecuteReader()
        {
            return q;
        }


        public void Take(int skipcount, string takecount)
        {
            q.Append(" OFFSET " + skipcount.ToString() + " ROWS FETCH NEXT " + takecount.ToString() + " ROWS ONLY");
        }

        public void Take(int takecount, int skipcount = 0)
        {
            q.Append($" OFFSET {skipcount} ROWS FETCH NEXT {takecount} ROWS ONLY");
        }

        public void UNION(Query query)
        {
            try
            {
                q.Append(" UNION " + query);
            }
            catch
            {

                throw;
            }
        }


        public void GroupBy(params string[] columns)
        {
            var fileds = string.Empty;
            foreach (var item in columns)
            {
                fileds += item + ",";
            }
            fileds = fileds.Remove(fileds.Length - 1);

            q.Append(" GROUP BY " + fileds);
        }
        //public void GroupByFileds(String table,params string[] columns)
        //{
        //    q = new Sql(string.Format("SELECT Sum({0}) FROM {1} ", fieldname, table + " "));
        //    //query.Sum(B2SData.Models.Generated.DTXN.TxnRespHistory.Schema, B2SData.Models.Generated.DTXN.TxnRespHistory.Columns.Count, true);
        //    var fileds = string.Empty;
        //    foreach (var item in columns)
        //    {
        //        fileds += item + ",";
        //    }
        //    fileds = fileds.Remove(fileds.Length - 1);

        //    q.Append(" GROUP BY " + fileds);
        //}
        public void Sum(string table, string fieldname, bool hasWithNolock)
        {
            q = new Sql(string.Format("SELECT Sum({0}) FROM {1} ", fieldname, table + " "));

            if (hasWithNolock)
                q = q.Append(" with(nolock) ");
            schem = table;
            isNew = true;
        }

        public void INNERJOIN(string tablename, Dictionary<string, string> fields)
        {
            q.Append(" INNER JOIN " + tablename + " ON ");

            foreach (var item in fields)
            {
                q.Append(item.Key + " = " + item.Value);
            }
        }
    }
}
