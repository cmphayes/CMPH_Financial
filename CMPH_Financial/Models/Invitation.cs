using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMPH_Financial.Models
{
    public class Invitation
    {
        public int Id { get; set; }

        public string HouseholdId { get; set; }

        public bool Accepted { get; set; }

        public Guid Code { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Expires { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        
        public string Sender { get; set; }
               
        public virtual Household Household { get; set; }
    }
}