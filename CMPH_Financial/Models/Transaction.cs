using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPH_Financial.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public DateTimeOffset TransactionTime { get; set; }
        public double TransactionAmount { get; set; }
        public bool Reconciled { get; set; }
        public double ReconciledAmount { get; set; }
        public DateTimeOffset ReconciledTime { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public bool Deleted { get; set; }

        public int AccountId { get; set; }
        public int TransactionTypeId { get; set; }
        public string ReconcilEnteredById { get; set; }
        public string EnteredById { get; set; }
        public string BudgetItemId { get; set; }

        public virtual TransactionType TransactionType { get; set; }
        public virtual Account Account { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }
        public virtual ApplicationUser ReconcilEnteredBy { get; set; }
    }

}