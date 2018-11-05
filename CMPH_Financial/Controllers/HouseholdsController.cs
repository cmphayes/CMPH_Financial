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
using CMPH_Financial.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AlertUser()
        {
            string userId = User.Identity.GetUserId();
            var alertEmail = db.Users.Find(userId).Email;

            try
            {
                var from = ConfigurationManager.AppSettings["emailfrom"];

                var email = new MailMessage(from, alertEmail)
                {
                    Subject = "An Account Is At Or Below $0.00",
                    Body = $"<p>One of your accounts is at or below $0.00 </p>",
                    IsBodyHtml = true
                };

                var svc = new PersonalEmail();
                await svc.SendAsync(email);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromResult(0);
            }

            return RedirectToAction("Details", "Households");
        }

        // GET: Invite
        public ActionResult Invite()
        {
            var userId = User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;

            var newInvite = new InvitationViewModel
            {
                HouseholdId = (int)householdId
            };

            return View(newInvite);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>  Invite(InvitationViewModel invitation)
        {
            var newInvitation = new Invitation
            {
                Email = invitation.ToEmail,
                HouseholdId = invitation.HouseholdId,
                Expires = DateTime.Now.AddDays(3),
                Created = DateTimeOffset.Now,
                Code = Guid.NewGuid(),
            };

            db.Invitations.Add(newInvitation);
            db.SaveChanges();
            try
            {
                var from = ConfigurationManager.AppSettings["emailfrom"];

                var callbackUrl = Url.Action("RegisterFromInvite", "Account", new { email = newInvitation.Email, code = newInvitation.Code }, protocol: Request.Url.Scheme);

                var email = new MailMessage(from, newInvitation.Email)
                {
                    Subject = "You have been invited to join a household.",
                    Body = $"<p> Email From: <bold>{invitation.FromName}</bold></p> <p>Subject:{invitation.Subject}</p> <p>Message:You have been invited to join a household.{invitation.Code}</p>< a href =\"" + callbackUrl + "\">here</a>",
                    IsBodyHtml = true
                };

                var svc = new PersonalEmail();
                await svc.SendAsync(email);

                db.Invitations.Add(newInvitation);
                db.SaveChanges();
                return RedirectToAction("Details", "Households");

            }

            catch (Exception ex)
            {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
            }

            return RedirectToAction("Details", "Households");
        }

      

        // GET: Households/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Household household)
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
                return RedirectToAction("Details", "Households");
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
                return RedirectToAction("Details", "Households");
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
            household.Deleted = true;
            db.SaveChanges();
            return RedirectToAction("Lobby");
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
