using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPH_Financial.Models
{
    public class Account
    {
        public int Id { get; set; }
        public double CurrentBalance { get; set; }
        public double InitialBalance { get; set; }
        public double ReconciledBalance { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }

        public int HouseholdId { get; set; }

        public virtual Household Household { get; set; }


        public virtual ICollection<Transaction> Transactions { get; set; }

        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}