using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;

namespace PublicationAssistantSystem.Web.Controllers
{
    public class FacultyController : Controller
    {
        private IPublicationRepository db;

        public FacultyController(IPublicationRepository db)
        {
            this.db = db;
        }

        // GET: Faculty/FacultyIndex
        public ActionResult FacultyIndex()
        {
            return View(db.Faculties.ToList());
        }

        
        // GET: Faculty/FacultyDetails/5
        public ActionResult FacultyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // GET: Faculty/FacultyCreate
        public ActionResult FacultyCreate()
        {
            return View();
        }

        // POST: Faculty/FacultyCreate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FacultyCreate([Bind(Include = "Institutes,Id,Name")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.Faculties.Add(faculty);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a Faculty record");
                return RedirectToAction("FacultyIndex");
            }

            DisplayErrorMessage();
            return View(faculty);
        }

        // GET: Faculty/FacultyEdit/5
        public ActionResult FacultyEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: FacultyFaculty/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FacultyEdit([Bind(Include = "Institutes,Id,Name")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faculty).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Faculty record");
                return RedirectToAction("FacultyIndex");
            }
            DisplayErrorMessage();
            return View(faculty);
        }

        //// GET: Faculty/FacultyDelete/5
        //public ActionResult FacultyDelete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Faculty faculty = db.Faculties.Find(id);
        //    if (faculty == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(faculty);
        //}

        // POST: Faculty/FacultyDelete/5
        [HttpPost, ActionName("FacultyDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult FacultyDeleteConfirmed(int id)
        {
            Faculty faculty = db.Faculties.Find(id);
            db.Faculties.Remove(faculty);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a Faculty record");
            return RedirectToAction("FacultyIndex");
        }

        private void DisplaySuccessMessage(string msgText)
        {
            TempData["SuccessMessage"] = msgText;
        }

        private void DisplayErrorMessage()
        {
            TempData["ErrorMessage"] = "Save changes was unsuccessful.";
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
