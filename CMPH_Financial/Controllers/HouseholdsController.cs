using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CMPH_Financial.Helpers;
using CMPH_Financial.Models;
using Microsoft.AspNet.Identity;

namespace CMPH_Financial.Controllers
{
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private HouseholdsHelper householdHelper = new HouseholdsHelper();


        // GET: Households
        public ActionResult Index()
        {           
            return View(db.Households.ToList());
        }

        // GET: Invite
        public ActionResult Invite()
        {
            return View(db.Households.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Invite(InviteEmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var to = model.ToEmail;
                    var from = "Financial<cmphayes@gmail.com>";
                    var email = new MailMessage(from, to)
                    {
                        Subject = model.Subject,
                        Body = $"<p> Email From: <bold>{model.FromName}</bold> ({model.FromEmail})</p><p> Subject:</p><p>{model.Subject}</p><p> Message:</p><p>{model.Body}</p><p>{model.Body}</p>",
                        IsBodyHtml = true
                    };


                    var svc = new InviteEmail();
                    await svc.SendAsync(email);

                    return View(new EmailModel());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }

        // GET: Households/Details/5
        [Authorize]
        public ActionResult Details()
        {
            var userId = User.Identity.GetUserId();
            if (db.Users.Find(userId).HouseholdId == null)
                return RedirectToAction("Index", "Home");

            return View(UserHelper.GetUserHousehold(userId));
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Password,ConfirmPassword")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Households.Add(household);
                db.SaveChanges();
                var headOfHouseHold = User.Identity.GetUserId();
                HouseholdsHelper.AddUserToHousehold(headOfHouseHold, household.Id);
                UserRoleHelper.AddUserToRole(headOfHouseHold, "HeadOfHouseHold");
                household.HouseholdCreatorId = headOfHouseHold;
                household.Created = DateTimeOffset.Now;
                return RedirectToAction("Index");
            }

            return View(household);
        }

        // GET: Households/Edit/5
        [Authorize(Roles = "Admin,HeadOfHousehold")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Created")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            db.Households.Remove(household);
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
