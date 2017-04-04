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
    public class BookCopiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookCopies
        public ActionResult Index(int? page)
        {
			int pageSize = 10;
			int pageNumber = (page ?? 1);
            var bookCopies = db.BookCopies.Include(b => b.Book);
            return View(bookCopies.OrderByDescending(m => m.LastUpdated).ToPagedList(pageNumber, pageSize));
        }

        // GET: BookCopies/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            return View(bookCopy);
        }

        // GET: BookCopies/Create
        public ActionResult Create()
        {
            ViewBag.BookID = new SelectList(db.Books, "ID", "Name");
            return View();
        }

        // POST: BookCopies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CopyNumber,BookID,Location,Available,LastUpdatedBy,LastUpdated")] BookCopy bookCopy)
        {
            if (ModelState.IsValid)
            {
                bookCopy.ID = Guid.NewGuid();
                db.BookCopies.Add(bookCopy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookID = new SelectList(db.Books, "ID", "Name", bookCopy.BookID);
            return View(bookCopy);
        }

        // GET: BookCopies/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookID = new SelectList(db.Books, "ID", "Name", bookCopy.BookID);
            return View(bookCopy);
        }

        // POST: BookCopies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CopyNumber,BookID,Location,Available,LastUpdatedBy,LastUpdated")] BookCopy bookCopy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookCopy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.Books, "ID", "Name", bookCopy.BookID);
            return View(bookCopy);
        }

        // GET: BookCopies/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            return View(bookCopy);
        }

        // POST: BookCopies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            BookCopy bookCopy = db.BookCopies.Find(id);
            db.BookCopies.Remove(bookCopy);
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
