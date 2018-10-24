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
        public string Date { get; set; }
        public int Amount { get; set; }
        public int Type { get; set; }
        public bool Reconciled { get; set; }
        public int ReconciledAmount { get; set; }
        public DateTime ReconciledTime { get; set; }
        public string Property { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public int CategoryId { get; set; }
        public int EnteredById { get; set; }
        public int AccountId { get; set; }
        public int ReconcilEnteredById { get; set; }

    }
}