using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPH_Financial.Models
{
    public class TransactionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }


        public virtual ICollection<Transaction> Transactions { get; set; }

        public TransactionType()
        {
            Transactions = new HashSet<Transaction>();
        }

        public enum Type
        {
            Withdrawal,
            Deposit,
            AdjustUp,
            AdjustDown,
        }
    }
}