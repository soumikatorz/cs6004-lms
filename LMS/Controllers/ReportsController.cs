using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult OldBooks()
        {
            return View();
        }

        public ActionResult InactiveBooks()
        {
            return View();
        }

        public ActionResult InactiveMembers()
        {
            return View();
        }


    }
}