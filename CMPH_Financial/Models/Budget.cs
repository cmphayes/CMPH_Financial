using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPH_Financial.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HouseholdId { get; set; }

        public virtual ICollection<BudgetItem> BudgetItems { get; set; }


        public Budget()
        {
            BudgetItems = new HashSet<BudgetItem>();
        }
    }
}