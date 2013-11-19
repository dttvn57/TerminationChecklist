using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TerminationChecklist.Models;

namespace TerminationChecklist.Controllers
{
    public class HomeController : Controller
    {
        // --- test GIT commit
        // test GIT commit 2
        // test GIT 3
        public ActionResult Index()
        {
            return View();  // RedirectToAction("Index", "Checklist");
        }

        public ActionResult Confirm()
        {
            return View();
        }
    }
}
