using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPH_Financial.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }

        public int Amount { get; set; }

        public int TransactionId { get; set; }

        public int CategoryId { get; set; }

        public int BudgetId { get; set; }

    }
}