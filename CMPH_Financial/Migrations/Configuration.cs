namespace CMPH_Financial.Migrations
{
    using CMPH_Financial.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CMPH_Financial.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CMPH_Financial.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            //create roles
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "HeadOfHouseHold"))
            {
                roleManager.Create(new IdentityRole { Name = "HeadOfHouseHold" });
            }
            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                roleManager.Create(new IdentityRole { Name = "Member" });
            }
            if (!context.Roles.Any(r => r.Name == "Child"))
            {
                roleManager.Create(new IdentityRole { Name = "Child" });
            }

            //create users
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            //create user to assign to admin role
            if (!context.Users.Any(u => u.Email == "HeadOfHouseHold@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "HeadOfHouseHold@Mailinator.com",
                    Email = "HeadOfHouseHold@Mailinator.com",
                    FirstName = "Head",
                    LastName = "OfHouseHold",
                    DisplayName = "Head1"
                }, "Abcd1234!");
            }
            var HeadOfHouseHoldId = userManager.FindByEmail("HeadOfHouseHold@Mailinator.com").Id;
            userManager.AddToRole(HeadOfHouseHoldId, "HeadOfHouseHold");

            if (!context.Users.Any(u => u.Email == "Member1@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Member1@Mailinator.com",
                    Email = "Member1@Mailinator.com",
                    FirstName = "Member1FirstName",
                    LastName = "Member1LastName",
                    DisplayName = "Member1"
                }, "Abcd1234!");
            }
            var Member1Id = userManager.FindByEmail("Member1@Mailinator.com").Id;
            userManager.AddToRole(Member1Id, "Member");

            if (!context.Users.Any(u => u.Email == "Member2@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Member2@Mailinator.com",
                    Email = "Member2@Mailinator.com",
                    FirstName = "Member2FirstName",
                    LastName = "Member2LastName",
                    DisplayName = "Member2"
                }, "Abcd1234!");
            }
            var Member2Id = userManager.FindByEmail("Member2@Mailinator.com").Id;
            userManager.AddToRole(Member2Id, "Member");

            //create user to assign to mod role
            if (!context.Users.Any(u => u.Email == "Child1@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Child1@Mailinator.com",
                    Email = "Child1@Mailinator.com",
                    FirstName = "Child1FirstName",
                    LastName = "Child1LastName",
                    DisplayName = "Child1"
                }, "Abcd1234!");
            }
            //assign users to roles
            var Child1Id = userManager.FindByEmail("Child1@Mailinator.com").Id;
            userManager.AddToRole(Child1Id, "Child");

            if (!context.Users.Any(u => u.Email == "CMPH@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "CMPH@Mailinator.com",
                    Email = "CMPH@Mailinator.com",
                    FirstName = "C",
                    LastName = "H",
                    DisplayName = "CMPH"
                }, "Abcd1234!");
            }
            //assign users to roles
            var CMPHId = userManager.FindByEmail("CMPH@Mailinator.com").Id;
            userManager.AddToRole(CMPHId, "Admin");

            context.BudgetItems.AddOrUpdate(bs => bs.Name,
            new BudgetItem { Name = "status1" },
            new BudgetItem { Name = "status2" },
            new BudgetItem { Name = "status3" },
            new BudgetItem { Name = "status4" }
            );

            context.TransactionTypes.AddOrUpdate(tt => tt.Type,
            new TransactionType { Type = "Incoming" },
            new TransactionType { Type = "OutGoing" }
            );

            context.Categories.AddOrUpdate(tp => tp.Name,
            new Category { Name = "priority1" },
            new Category { Name = "priority2" },
            new Category { Name = "priority3" },
            new Category { Name = "priority4" }
            );
        }
    }

}