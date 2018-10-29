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
                    DisplayName = "HeadOfHousehold1",
                    HouseholdId = 999 
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
                    DisplayName = "Member1",
                    HouseholdId = 999

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
                    DisplayName = "Member2",                    
                    HouseholdId = 999
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
                    DisplayName = "Child1",
                    HouseholdId = 999
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
                    DisplayName = "CMPH",
                    HouseholdId = 999
                }, "Abcd1234!");
            }
            //assign users to roles
            var CMPHId = userManager.FindByEmail("CMPH@Mailinator.com").Id;
            userManager.AddToRole(CMPHId, "Admin");

            //First seed a Demo House
            context.Households.AddOrUpdate(h => h.Name,
                new Household { Id = 999, Name = "Demo House", Created= DateTimeOffset.Now }
            );

            context.Budgets.AddOrUpdate(b => b.Name,
                new Budget { Id = 1000, HouseholdId = 999, Name = "Demo Budget 1", TargetBudget = 500 },
                new Budget { Id = 2000, HouseholdId = 999, Name = "Demo Budget 2", TargetBudget = 1000 },
                new Budget { Id = 3000, HouseholdId = 999, Name = "Demo Budget 3", TargetBudget = 1500 }
            );

            context.Categories.AddOrUpdate(tp => tp.Name,
                new Category { Id = 1000, Name = "Category1" },
                new Category { Id = 2000, Name = "Category2" },
                new Category { Id = 3000, Name = "Category3" }

            );

            //Your BudgetItems need to reference a Budget
            context.BudgetItems.AddOrUpdate(bs => bs.Name,
            new BudgetItem { BudgetId = 1000, CategoryId = 1000, Name = "Budget Item 1" },
            new BudgetItem { BudgetId = 1000, CategoryId = 2000, Name = "Budget Item 2" },
            new BudgetItem { BudgetId = 2000, CategoryId = 3000, Name = "Budget Item 3" },
            new BudgetItem { BudgetId = 2000, CategoryId = 1000, Name = "Budget Item 4" },
            new BudgetItem { BudgetId = 3000, CategoryId = 2000, Name = "Budget Item 5" },
            new BudgetItem { BudgetId = 3000, CategoryId = 3000, Name = "Budget Item 6" }

            );

            context.TransactionTypes.AddOrUpdate(tt => tt.Type,
            new TransactionType { Type = "Incoming" },
            new TransactionType { Type = "OutGoing" }
            );


        }
    }

}