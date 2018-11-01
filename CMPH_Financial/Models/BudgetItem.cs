using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPH_Financial.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }
        public double CurrentBalance { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }


        //public int TransactionId { get; set; }
        public int BudgetId { get; set; }

        //public virtual Transaction Transaction { get; set; }
        public virtual Budget Budget { get; set; }

    }
}