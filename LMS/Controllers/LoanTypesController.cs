using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using PagedList;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMS.Models;

namespace LMS.Controllers
{
    public class LoanTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LoanTypes
        public ActionResult Index(int? page)
        {
			int pageSize = 10;
			int pageNumber = (page ?? 1);
            return View(db.LoanTypes.OrderByDescending(m => m.LastUpdated).ToPagedList(pageNumber, pageSize));
        }

        // GET: LoanTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanType loanType = db.LoanTypes.Find(id);
            if (loanType == null)
            {
                return HttpNotFound();
            }
            return View(loanType);
        }

        // GET: LoanTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoanTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Duration,LastUpdatedBy,LastUpdated")] LoanType loanType)
        {
            if (ModelState.IsValid)
            {
                loanType.ID = Guid.NewGuid();
                db.LoanTypes.Add(loanType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loanType);
        }

        // GET: LoanTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanType loanType = db.LoanTypes.Find(id);
            if (loanType == null)
            {
                return HttpNotFound();
            }
            return View(loanType);
        }

        // POST: LoanTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Duration,LastUpdatedBy,LastUpdated")] LoanType loanType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loanType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loanType);
        }

        // GET: LoanTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanType loanType = db.LoanTypes.Find(id);
            if (loanType == null)
            {
                return HttpNotFound();
            }
            return View(loanType);
        }

        // POST: LoanTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            LoanType loanType = db.LoanTypes.Find(id);
            db.LoanTypes.Remove(loanType);
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
