using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMPH_Financial.Models;
using Microsoft.AspNet.Identity;
using CMPH_Financial.Helpers;


namespace CMPH_Financial.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            return View(db.Transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Name");
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name");
            ViewBag.BudgetItemId = new SelectList(db.BudgetItems, "Id", "Name");

            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Date,Amount,Type,Reconciled,ReconciledAmount,CategoryId,EnteredById,AccountId,ReconcilEnteredById")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);

                var bankAccount = db.Accounts.Find(transaction.AccountId).HouseholdId;
                transaction.TransactionTime = DateTime.Now;
                transaction.EnteredById = User.Identity.GetUserId();
                string userId = transaction.ReconcilEnteredById;
                db.Transactions.Add(transaction);
                db.SaveChanges();

                if(bankAccount < 0)
                {
                    return RedirectToAction("AlertUser", "Households");
                }

                return RedirectToAction("Details", "Households");
            }

            return View(transaction);
        }

        // GET: Transactions/Void
        public ActionResult Void(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //Post: Transaction/Void
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Void( Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if (transaction.ReconciledAmount > 0)
                {
                    BudgetHelper.VoidAdjustBalance(transaction.ReconciledAmount);
                    BankAccountHelper.VoidAdjustBalance(transaction.ReconciledAmount);
                }
                else
                {
                    BudgetHelper.VoidAdjustBalance(transaction.TransactionAmount);
                    BankAccountHelper.VoidAdjustBalance(transaction.TransactionAmount);

                }
                db.Entry(transaction).State = EntityState.Modified;
                transaction.Void = true;
                transaction.VoidedById = User.Identity.GetUserId();
                transaction.VoidTime = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Details", "Household");
            }
            return RedirectToAction("Details", "Household");

        }


        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Date,Amount,Type,Reconciled,ReconciledAmount,CategoryId,EnteredById,AccountId,ReconcilEnteredById")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                transaction.ReconcilEnteredById = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("Details", "Households");
            }
            return View(transaction);
        }


        // GET: Transactions/Reconcil/5
        public ActionResult Reconcil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Reconcil/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reconcil([Bind(Include = "Id,Description,Date,Amount,Type,Reconciled,ReconciledAmount,CategoryId,EnteredById,AccountId,ReconcilEnteredById")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                transaction.Reconciled = true;
                transaction.ReconcilEnteredById = User.Identity.GetUserId();
                transaction.ReconciledTime = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Details", "Households");
            }
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            transaction.Deleted = true;
            db.SaveChanges();
            return RedirectToAction("Details", "Households");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
