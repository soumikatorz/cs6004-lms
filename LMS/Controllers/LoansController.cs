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
    public class LoansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Loans
        public ActionResult Index(int? page)
        {
			int pageSize = 10;
			int pageNumber = (page ?? 1);
            var loans = db.Loans.Include(l => l.BookCopy).Include(l => l.LoanType).Include(l => l.Member);
            return View(loans.OrderByDescending(m => m.LastUpdated).ToPagedList(pageNumber, pageSize));
        }

        // GET: Loans/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        public ActionResult Create()
        {
            ViewBag.BookCopyID = new SelectList(db.BookCopies, "ID", "Location");
            ViewBag.LoanTypeID = new SelectList(db.LoanTypes, "ID", "Name");
            ViewBag.MemberID = new SelectList(db.Members, "ID", "FirstName");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BookCopyID,MemberID,IssuedOn,ReturnedOn,LoanCharge,PenaltyCharge,DueDate,LoanTypeID,LoanedByID,LastUpdatedBy,LastUpdated")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.ID = Guid.NewGuid();
                db.Loans.Add(loan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookCopyID = new SelectList(db.BookCopies, "ID", "Location", loan.BookCopyID);
            ViewBag.LoanTypeID = new SelectList(db.LoanTypes, "ID", "Name", loan.LoanTypeID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "FirstName", loan.MemberID);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookCopyID = new SelectList(db.BookCopies, "ID", "Location", loan.BookCopyID);
            ViewBag.LoanTypeID = new SelectList(db.LoanTypes, "ID", "Name", loan.LoanTypeID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "FirstName", loan.MemberID);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BookCopyID,MemberID,IssuedOn,ReturnedOn,LoanCharge,PenaltyCharge,DueDate,LoanTypeID,LoanedByID,LastUpdatedBy,LastUpdated")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookCopyID = new SelectList(db.BookCopies, "ID", "Location", loan.BookCopyID);
            ViewBag.LoanTypeID = new SelectList(db.LoanTypes, "ID", "Name", loan.LoanTypeID);
            ViewBag.MemberID = new SelectList(db.Members, "ID", "FirstName", loan.MemberID);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
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
