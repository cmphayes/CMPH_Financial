﻿using CMPH_Financial.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMPH_Financial.Helpers
{
   
        public class HouseholdsHelper
        {
            private static ApplicationDbContext db = new ApplicationDbContext();
            private static UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));


            public static bool IsUserOnThisHousehold(string userId, int HouseholdId)
            {
                var Household = db.Households.Find(HouseholdId);
                var flag = Household.Users.Any(u => u.Id == userId);
                return (flag);
            }

            public static bool IsUserOnAHousehold(string userId)
            {
                //var Household = db.Households.Find(userId);
                var newUser = db.Users.Find(userId);
                var flag = db.Households.Find().Users.Contains(newUser); 
                return (flag);
            }

            public void AddUserToHousehold(string userId, int HouseholdId)
            {
                if (!IsUserOnThisHousehold(userId, HouseholdId))
                {
                    Household hhold = db.Households.Find(HouseholdId);
                    var newUser = db.Users.Find(userId);

                    hhold.Users.Add(newUser);
                    db.SaveChanges();
                }
            }

            public static void AddAccountToHousehold(string accountId, int HouseholdId)
            {
                if (!IsUserOnThisHousehold(accountId, HouseholdId))
                {
                    Household hhold = db.Households.Find(HouseholdId);
                    var newAccount = db.Accounts.Find(accountId);

                    hhold.Accounts.Add(newAccount);
                    db.SaveChanges();
                }
            }

            public static void AddBudgetToHousehold(string budgetId, int HouseholdId)
            {
                if (!IsUserOnThisHousehold(budgetId, HouseholdId))
                {
                    Household hhold = db.Households.Find(HouseholdId);
                    var newBudget = db.Budgets.Find(budgetId);

                    hhold.Budgets.Add(newBudget);
                    db.SaveChanges();
                }
            }

            [ValidateAntiForgeryToken]
            public static void RemoveUserFromHousehold(string userId, int HouseholdId)
            {
                if (IsUserOnThisHousehold(userId, HouseholdId))
                {
                    Household hhold = db.Households.Find(HouseholdId);
                    var delUser = db.Users.Find(userId);

                    hhold.Users.Remove(delUser);
                    db.Entry(hhold).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            [ValidateAntiForgeryToken]
            public static void RemoveAccountFromHousehold(string accountId, int HouseholdId)
            {
                if (IsUserOnThisHousehold(accountId, HouseholdId))
                {
                    Household hhold = db.Households.Find(HouseholdId);
                    var delAccount = db.Accounts.Find(accountId);

                    hhold.Accounts.Remove(delAccount);
                    db.Entry(hhold).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            [ValidateAntiForgeryToken]
            public static void RemoveBudgetFromHousehold(string budgetId, int HouseholdId)
            {
                if (IsUserOnThisHousehold(budgetId, HouseholdId))
                {
                    Household hhold = db.Households.Find(HouseholdId);
                    var delBudget = db.Budgets.Find(budgetId);

                    hhold.Budgets.Remove(delBudget);
                    db.Entry(hhold).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            public ICollection<Household> ListUserHouseholds(string userId)
            {

                ApplicationUser user = db.Users.Find(userId);
                var households = db.Households.Where(u => u.Id == db.Users.Find(user).HouseholdId).ToList();
                return households;
            }

            public ICollection<Household> ListUserCreatedHouseholds(string userId)
            {
                return db.Households.Where(p => p.HouseholdCreatorId == userId).ToList();
            }

            //partials

            //public ICollection<Household> ListUserHouseholdsPartial(string userId)
            //{
            //    ApplicationUser user = db.Users.Find(userId);
            //    var Households = user.HouseholdId;
            //    return (HouseholdId);
            //}

            public ICollection<Household> ListUserCreatedHouseholdsPartial(string userId)
            {
                return db.Households.Where(p => p.HouseholdCreatorId == userId).ToList();
            }

            public ICollection<ApplicationUser> ListUsersOnHousehold(int HouseholdId)
            {
                return db.Households.Find(HouseholdId).Users;
            }

            //public ICollection<ApplicationUser> ListUsersNotOnHousehold(int HouseholdId)
            //{
            //    return db.Users.Where(u => u.Households.All(p => p.Id != HouseholdId)).ToList();
            //}

            //public ICollection<Household> ListUsersOnHousehold(string userId)
            //{
            //    ApplicationUser user = db.Users.Find(userId);
            //    var Households = user.Households.ToList();
            //    return (Households);
            //}


            //public static List<User> UserOnHousehold(int id)
            //{
            //    return db.Users.Where(t => t.HouseholdId == id).ToList();
            //}

            public static string GetHouseholdOwner(int HouseholdId)
            {
                var none = "No Household Owner Listed";
                var HouseholdOwnerId = db.Households.Find(HouseholdId).HouseholdCreatorId;
                var HouseholdOwner = db.Users.Find(HouseholdOwnerId).DisplayName;
                if (HouseholdOwner == null)
                {
                    return none;
                }
                if (string.IsNullOrEmpty(HouseholdOwner))
                {
                    return none;
                }
                return HouseholdOwner;
            }
        }
    
}