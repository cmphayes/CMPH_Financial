﻿using CMPH_Financial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMPH_Financial.Helpers
{
    public class BudgetItemHelper
    {

        private static ApplicationDbContext db = new ApplicationDbContext();

        public static void AdjustBalance(double transactionId)
        {
            var transaction = db.Transactions.Find(transactionId);
            var transactionType = transaction.TransactionType;
            var budgetItem = db.Accounts.Find(transaction.BudgetItemId);
            var bankId = transaction.AccountId;
            var bankAccount = db.Accounts.Find(bankId);

            db.Accounts.Attach(bankAccount);

            if (transactionType.Name == "Deposit")
                budgetItem.CurrentBalance += transaction.TransactionAmount;

            else if (transactionType.Name == "Withdrawl")
                budgetItem.CurrentBalance -= transaction.TransactionAmount;

            else if (transactionType.Name == "AdjustmentUp")
                budgetItem.CurrentBalance += transaction.TransactionAmount;

            else if (transactionType.Name == "AdjustmentDown")
                budgetItem.CurrentBalance -= transaction.TransactionAmount;


        }


        public static void VoidAdjustBalance(double amount)
        {
            var transaction = db.Transactions.Find(amount);
            var transactionType = transaction.TransactionType;
            var budgetItem = db.Accounts.Find(transaction.BudgetItemId);
            var bankId = transaction.AccountId;
            var bankAccount = db.Accounts.Find(bankId);

            db.Accounts.Attach(bankAccount);

            if (transactionType.Name == "Deposit")
                budgetItem.CurrentBalance -= amount;

            else if (transactionType.Name == "Withdrawl")
                budgetItem.CurrentBalance += amount;

            else if (transactionType.Name == "AdjustmentUp")
                budgetItem.CurrentBalance -= amount;

            else if (transactionType.Name == "AdjustmentDown")
                budgetItem.CurrentBalance += amount;


        }
    }
}