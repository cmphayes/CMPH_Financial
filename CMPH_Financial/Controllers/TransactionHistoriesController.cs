using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMPH_Financial.Models;

namespace CMPH_Financial.Controllers
{
    public class TransactionHistoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TransactionHistories
        public ActionResult Index()
        {
            return View(db.TransactionHistories.ToList());
        }

        // GET: TransactionHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionHistory transactionHistory = db.TransactionHistories.Find(id);
            if (transactionHistory == null)
            {
                return HttpNotFound();
            }
            return View(transactionHistory);
        }

        // GET: TransactionHistories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Date,Amount,Type,Reconciled,ReconciledAmount,ReconciledTime,Property,OldValue,NewValue,CategoryId,EnteredById,AccountId,ReconcilEnteredById")] TransactionHistory transactionHistory)
        {
            if (ModelState.IsValid)
            {
                db.TransactionHistories.Add(transactionHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transactionHistory);
        }

        // GET: TransactionHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionHistory transactionHistory = db.TransactionHistories.Find(id);
            if (transactionHistory == null)
            {
                return HttpNotFound();
            }
            return View(transactionHistory);
        }

        // POST: TransactionHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Date,Amount,Type,Reconciled,ReconciledAmount,ReconciledTime,Property,OldValue,NewValue,CategoryId,EnteredById,AccountId,ReconcilEnteredById")] TransactionHistory transactionHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transactionHistory);
        }

        // GET: TransactionHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionHistory transactionHistory = db.TransactionHistories.Find(id);
            if (transactionHistory == null)
            {
                return HttpNotFound();
            }
            return View(transactionHistory);
        }

        // POST: TransactionHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionHistory transactionHistory = db.TransactionHistories.Find(id);
            db.TransactionHistories.Remove(transactionHistory);
            db.SaveChanges();
            return RedirectToAction("Index");
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
