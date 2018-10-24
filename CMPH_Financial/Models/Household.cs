using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPH_Financial.Models
{
    public class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        //public string HeadOfHousehold { get; set; }




        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }

        public Household()
        {
            Users = new HashSet<ApplicationUser>();
            Accounts = new HashSet<Account>();
            Budgets = new HashSet<Budget>();


        }

    }
}