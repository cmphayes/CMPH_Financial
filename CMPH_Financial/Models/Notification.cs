using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPH_Financial.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int HouseholdId { get; set; }
        public string RecipientId { get; set; }
        public bool Read { get; set; }
        public bool Deleted { get; set; }

        public virtual Household Household { get; set; }
        public virtual ApplicationUser Recipient { get; set; }




    }
}