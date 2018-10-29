using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPH_Financial.Models
{
    public class TransactionHistory
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset TransactionTime { get; set; }
        public double Amount { get; set; }
        public int TransactionType { get; set; }
        public bool Reconciled { get; set; }
        public double ReconciledAmount { get; set; }
        public DateTimeOffset ReconciledTime { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public bool Deleted { get; set; }


        public int CategoryId { get; set; }
        public string EnteredById { get; set; }
        public int AccountId { get; set; }
        public string ReconcilEnteredById { get; set; }

        public virtual Category Category { get; set; }
        public virtual Account Account { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }
        public virtual ApplicationUser ReconcilEnteredBy { get; set; }


    }
}