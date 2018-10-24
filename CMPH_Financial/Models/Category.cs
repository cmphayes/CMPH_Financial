using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPH_Financial.Models
{
    public class Category
    {
        public string Name { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }


        public Category()
        {
            BudgetItems = new HashSet<BudgetItem>();
            Transactions = new HashSet<Transaction>();

        }
    }
}