using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMPH_Financial.ViewModels
{
    public class InvitationViewModel
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
            public string FromEmail { get; set; }

            [Required]
            [Display(Name = "Email")]
            [EmailAddress]
            public string ToEmail { get; set; }

            public string FromName { get; set; }

            public string Subject { get; set; }

            public string Body { get; set; }

    }
}