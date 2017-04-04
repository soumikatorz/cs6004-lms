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
    public class PressController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Press
        public ActionResult Index(int? page)
        {
			int pageSize = 10;
			int pageNumber = (page ?? 1);
            return View(db.Press.OrderByDescending(m => m.LastUpdated).ToPagedList(pageNumber, pageSize));
        }

        // GET: Press/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Press press = db.Press.Find(id);
            if (press == null)
            {
                return HttpNotFound();
            }
            return View(press);
        }

        // GET: Press/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Press/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,LastUpdatedBy,LastUpdated")] Press press)
        {
            if (ModelState.IsValid)
            {
                press.ID = Guid.NewGuid();
                db.Press.Add(press);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(press);
        }

        // GET: Press/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Press press = db.Press.Find(id);
            if (press == null)
            {
                return HttpNotFound();
            }
            return View(press);
        }

        // POST: Press/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,LastUpdatedBy,LastUpdated")] Press press)
        {
            if (ModelState.IsValid)
            {
                db.Entry(press).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(press);
        }

        // GET: Press/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Press press = db.Press.Find(id);
            if (press == null)
            {
                return HttpNotFound();
            }
            return View(press);
        }

        // POST: Press/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Press press = db.Press.Find(id);
            db.Press.Remove(press);
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
