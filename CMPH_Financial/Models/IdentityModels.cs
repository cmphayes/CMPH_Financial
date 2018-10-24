﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CMPH_Financial.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HouseholdId { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string DisplayName { get; set; }




        public virtual ICollection<Transaction> Transactions { get; set; }
        //public virtual ICollection<Ticket> Tickets { get; set; }
        //public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        //public virtual ICollection<TicketComment> TicketComments { get; set; }
        //public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        //public virtual ICollection<TicketNotification> TicketNotifications { get; set; }


        public ApplicationUser()
        {
            Transactions = new HashSet<Transaction>();
            //Tickets = new HashSet<Ticket>();
            //TicketAttachments = new HashSet<TicketAttachment>();
            //TicketComments = new HashSet<TicketComment>();
            //TicketHistories = new HashSet<TicketHistory>();
            //TicketNotifications = new HashSet<TicketNotification>();




        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            userIdentity.AddClaim(new Claim("FirstName", this.FirstName));
            userIdentity.AddClaim(new Claim("LastName", this.LastName));
            //userIdentity.AddClaim(new Claim("DisplayName", this.DisplayName));
            userIdentity.AddClaim(new Claim("UserName", this.UserName));
            //userIdentity.AddClaim(new Claim("ProfileImagePath", this.ProfileImagePath));
            userIdentity.AddClaim(new Claim("FullName", $"{this.FirstName}{this.LastName}"));

            // Add custom user claims here      
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<CMPH_Financial.Models.Account> Accounts { get; set; }

        public System.Data.Entity.DbSet<CMPH_Financial.Models.Budget> Budgets { get; set; }

        public System.Data.Entity.DbSet<CMPH_Financial.Models.BudgetItem> BudgetItems { get; set; }

        public System.Data.Entity.DbSet<CMPH_Financial.Models.Household> Households { get; set; }

        public System.Data.Entity.DbSet<CMPH_Financial.Models.Transaction> Transactions { get; set; }

        public System.Data.Entity.DbSet<CMPH_Financial.Models.TransactionHistory> TransactionHistories { get; set; }

        //    public DbSet<Ticket> Tickets { get; set; }

        //    public DbSet<Project> Projects { get; set; }

        //    public DbSet<CMPH_BugTracker.Models.TicketPriority> TicketPriorities { get; set; }

        //    public DbSet<CMPH_BugTracker.Models.TicketStatus> TicketStatus { get; set; }

        //    public DbSet<CMPH_BugTracker.Models.TicketType> TicketTypes { get; set; }

        //    public DbSet<CMPH_BugTracker.Models.TicketAttachment> TicketAttachments { get; set; }

        //    public DbSet<CMPH_BugTracker.Models.TicketComment> TicketComments { get; set; }

        //    public DbSet<CMPH_BugTracker.Models.TicketHistory> TicketHistories { get; set; }

        //    public DbSet<CMPH_BugTracker.Models.TicketNotification> TicketNotifications { get; set; }
        //
    }
}