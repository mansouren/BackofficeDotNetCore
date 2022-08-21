using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.B2STxn.Other
{
    public enum TransactionType
    {
        successful = 1,
        fail = 2,
        All = 3,
        Yesterday = 4
    }

    public class TxnBrief
    {
        public DateTime StartTime { get; set; }
        public int Value { get; set; }
    }

    public class TxnBriefByPoscon
    {
        public DateTime StartTime;
        public int Poscondition;
        public int Value;
        public string Title;

    }
}
