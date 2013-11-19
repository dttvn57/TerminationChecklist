using Rotativa;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TerminationChecklist.Models;

namespace TerminationChecklist.Controllers
{
    public class ChecklistController : Controller
    {
        TerminationChecklistDB _db = new TerminationChecklistDB();

        //private static List<Checklist> _ChecklistList;

        //public static List<Checklist> ChecklistList
        //{
        //    get
        //    {
        //        if (_ChecklistList == null)
        //        {
        //            _ChecklistList = new List<Checklist>();
        //            _ChecklistList.Add(new Checklist
        //            {
        //                Name = "Shemeer",
        //            });
        //        }
        //        return _ChecklistList;
        //    }
        //    set { _ChecklistList = value; }
        //}

        //
        // GET: /Checklist/

        public ActionResult GetAllForms(string lastName, string firstName)
        {
            bool isManager = IsHRManager(lastName, firstName);
            bool isMember = IsHRMember(lastName, firstName);
            if (!isManager && !isMember)
                return PartialView("_AllForms", null);

            TempData["ISMANAGER"] = isManager ? "yes" : "no";

            if (isManager)
            {
                var itemList = from f in _db.Checklists
                               orderby f.created_date descending
                               select f;
                return PartialView("_AllForms", itemList.ToList());
            }
            else
            {
                var itemList = from f in _db.Checklists
                               where f.created_by_fname.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                                      f.created_by_lname.Equals(lastName, StringComparison.OrdinalIgnoreCase)
                               orderby f.created_date descending
                               select f;
                return PartialView("_AllForms", itemList.ToList());
            }
            //var list = _db.Checklists.Select(f => (f.created_by_fname.Equals(firstName, StringComparison.OrdinalIgnoreCase)) && (f.created_by_lname.Equals(lastName, StringComparison.OrdinalIgnoreCase)));
            //return PartialView("_AllForms", list.ToList());
            //OrderByDescending(f => f.created_date).ToList());

            //return View(_db.Checklists);      //ChecklistList);
        }

        //
        // GET: /Checklist/Details/5

        public ActionResult Details(int id)
        {
            return View("Form", _db.Checklists.Where(c => c.ID == id).FirstOrDefault());
        }

        public ActionResult Edit(int id, bool isManager)
        {
            TempData["EDITMODE"] = "edit";
            TempData["ISMANAGER"] = isManager ? "yes" : "no";
            return View("Form", _db.Checklists.Where(c => c.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(Checklist chklist)
        {
            try
            {
                _db.Entry(chklist).State = System.Data.EntityState.Modified;
                _db.SaveChanges();
                TempData["STATUS"] = "Modified form is saved.";
                return RedirectToAction("Confirm", "Home");
            }
            catch
            {
            }
            TempData["STATUS"] = "Error in saving form";
            return PartialView("_AllForms", null);
        }

        //
        // GET: /Checklist/Create

        public ActionResult Create(string lastName, string firstName)
        {
            // any member can create (including mamagers)
            bool isMember = IsHRMember(lastName, firstName);
            if (isMember)
            {
                TempData["EDITMODE"] = "create";
                TempData["CREATED_BY_LNAME"] = lastName;
                TempData["CREATED_BY_FNAME"] = firstName;
                return View("Form");
            }

            TempData["STATUS"] = "You're not authorized.";
            return PartialView("_AllForms", null);
        }

        //
        // POST: /Checklist/Create

        [HttpPost]
        public ActionResult Create(Checklist chklist)
        {
            try
            {
                chklist.created_date = DateTime.Now;
                chklist.created_by_lname = (string)TempData["CREATED_BY_LNAME"];
                chklist.created_by_fname = (string)TempData["CREATED_BY_FNAME"];
                _db.Checklists.Add(chklist);
                _db.SaveChanges();
            }
            catch
            {
            }

            TempData["STATUS"] = "Form has been sent to the manager.";
            return RedirectToAction("Confirm", "Home");
        }

        //
        // GET: /Checklist/Edit/5

        public ActionResult Approve(int id, bool isManager)
        {
            //bool isManager = IsHRManager(lastName, firstName);
            if (isManager)
            {
                TempData["EDITMODE"] = "approve";
                return View("Form", _db.Checklists.Where(c => c.ID == id).FirstOrDefault());
            }

            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Checklist/Edit/5

        [HttpPost]
        public ActionResult Approve(int id, Checklist chklist)
        {
            try
            {
                if (chklist.supervisor_signed_date == null)
                    chklist.supervisor_signed_date = DateTime.Now;

                _db.Entry(chklist).State = System.Data.EntityState.Modified;
                _db.SaveChanges();

                TempData["STATUS"] = "Form is saved.";
                return RedirectToAction("Confirm", "Home");
            }
            catch
            {
                TempData["STATUS"] = "Error in saing form.";
                return RedirectToAction("Confirm", "Home");
            }
        }

        //
        // GET: /Checklist/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                ////////_db.Checklists.Remove(c => c.ID == id);
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // POST: /Checklist/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Print(int id)
        {
            Checklist chklist = _db.Checklists.Find(id);
            if (chklist == null)
            {
                TempData["STATUS"] = "Form not Found.";
                return PartialView("_AllForms", null);
            }

            return View("Print", chklist);
        }

        public ActionResult PrintForm(int id)
        {
            Checklist chklist = _db.Checklists.Find(id);
            return new ActionAsPdf(
                           "Print",
                           new { id = id }) { FileName = chklist.Name == "" ? id.ToString() + "_Termination Checklist.pdf" : chklist.Name + "_Termination Checklist.pdf" };
        }


        private bool IsHRManager(string lastName, string firstName)
        {
            string[] names = ConfigurationManager.AppSettings.AllKeys
                              .Where(key => key.StartsWith("HR_Manager"))
                              .Select(key => ConfigurationManager.AppSettings[key])
                              .ToArray();
            foreach (string name in names)
            {
                string last = "", first = "";
                ParseName(name, ref last, ref first);
                if (last.Equals(lastName, StringComparison.OrdinalIgnoreCase) && first.Equals(firstName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private bool IsHRMember(string lastName, string firstName)
        {
            string[] names = ConfigurationManager.AppSettings.AllKeys
                              .Where(key => key.StartsWith("HR_Manager"))
                              .Select(key => ConfigurationManager.AppSettings[key])
                              .ToArray();
            foreach (string name in names)
            {
                string last = "", first = "";
                ParseName(name, ref last, ref first);
                if (last.Equals(lastName, StringComparison.OrdinalIgnoreCase) && first.Equals(firstName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            names = ConfigurationManager.AppSettings.AllKeys
                             .Where(key => key.StartsWith("HR_Member"))
                             .Select(key => ConfigurationManager.AppSettings[key])
                             .ToArray();
            foreach (string name in names)
            {
                string last = "", first = "";
                ParseName(name, ref last, ref first);
                if (last.Equals(lastName, StringComparison.OrdinalIgnoreCase) && first.Equals(firstName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private void ParseName(string full, ref string last, ref string first)
        {
            int commaInd = full.IndexOf(',');
            last = full.Substring(0, commaInd);
            first = full.Substring(commaInd + 1, full.Length - last.Length - 1);
            last = last.Trim();
            first = first.Trim();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
