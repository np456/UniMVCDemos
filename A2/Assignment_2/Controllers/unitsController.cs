using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment_2.Models;
using System.Web.Security;
using System.IO;

namespace Assignment_2.Controllers
{
    [Authorize(Roles ="Admin")]
    public class unitsController : Controller
    {
        private A2Entities db = new A2Entities();

        // GET: units
        public ActionResult Index()
        {
            return View(db.units.ToList());
        }

        // GET: units/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            unit unit = db.units.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // GET: units/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // Validate unit code is unique
        [HttpPost]
        public JsonResult IsUnitCodeAvailable(string unit_code)
        {
            return Json(!db.units.Any(unit => unit.unit_code == unit_code), JsonRequestBehavior.AllowGet);

        }

        // Deleted unit title is unique check as realized can have under/post grad same unit title but different unit code

        // POST: units/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "unit_code,unit_title,unit_coordinator,unit_outline_pdf_document")] unit unit, HttpPostedFileBase pdf)
        {

            if (ModelState.IsValid)
            {
                if (pdf != null && pdf.ContentLength < (256 * 1024 * 8) && pdf.ContentType.Contains("application/pdf"))
                {
                    var fileName = Path.GetFileName(pdf.FileName);
                    var path = Server.MapPath("~/UnitOutlines/" + fileName);
                    pdf.SaveAs(path);
                    unit.unit_outline_pdf_document = "~/UnitOutlines/" + fileName;
                    
                    db.units.Add(unit);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                else
                    ViewBag.FileInvalid = "Please select a unit outline of type .pdf less than 2MB.";
            }

            return View(unit);
        }

        // GET: units/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            unit unit = db.units.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "unit_code,unit_title,unit_coordinator,unit_outline_pdf_document")] unit unit, HttpPostedFileBase pdf)
        {
            if (ModelState.IsValid)
            {
                if (pdf != null && pdf.ContentLength < (256 * 1024 * 8) && pdf.ContentType.Contains("application/pdf"))
                {
                    var fileName = Path.GetFileName(pdf.FileName);
                    var path = Server.MapPath("~/UnitOutlines/" + fileName);
                    pdf.SaveAs(path);
                    unit.unit_outline_pdf_document = "~/UnitOutlines/" + fileName;

                    db.Entry(unit).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else if (pdf == null)
                {
                    db.Entry(unit).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    ViewBag.FileInvalid = "Please select a unit outline of type .pdf less than 2MB."; 

            }
            return View(unit);
        }

        // GET: units/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            unit unit = db.units.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            unit unit = db.units.Find(id);
            db.units.Remove(unit);
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
