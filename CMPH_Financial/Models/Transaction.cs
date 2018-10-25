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
        public DateTime TransactionTime { get; set; }
        public double TransactionAmount { get; set; }
        public bool Reconciled { get; set; }
        public double ReconciledAmount { get; set; }
        public DateTime ReconciledTime { get; set; }
        public string Property { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public int CategoryId { get; set; }
        public int EnteredById { get; set; }
        public int AccountId { get; set; }
        public int ReconcilEnteredById { get; set; }
        public int TransactionTypeId { get; set; }


        public virtual TransactionType TransactionType { get; set; }
        public virtual Category Category { get; set; }
        public virtual Account Account { get; set; }
    }

}