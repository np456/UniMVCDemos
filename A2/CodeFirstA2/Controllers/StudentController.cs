using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeFirstA2.DataAccessLayer;
using CodeFirstA2.Models;

namespace CodeFirstA2.Controllers
{
    public class StudentController : Controller
    {
        // Class variable created that instantiates a database context object
        private UniversityContext db = new UniversityContext();

        // GET: Student
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // Validate student id is unique
        [HttpPost]
        public JsonResult IsStudentIDAvailable(int studentId)
        {
            return Json(!db.Students.Any(s => s.StudentId == studentId), JsonRequestBehavior.AllowGet);

        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,Photo")] Student student, HttpPostedFileBase image)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Students.Add(student);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            try
            {
                if (ModelState.IsValid)
                {
                    if (image != null && image.ContentLength < (256 * 1024) && image.ContentType.Contains("image"))
                    {

                        var fileName = Path.GetFileName(image.FileName);
                        var path = Server.MapPath("~/Photos/" + fileName);
                        image.SaveAs(path);
                        student.Photo = "~/Photos/" + fileName;

                        db.Students.Add(student);
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else
                        ViewBag.ImageInvalid = "Please upload a photo of type .gif, .png, .jpeg, .bmp or .webp of less than 256kb.";


                }

            }
            catch (DataException /* dex*/ )
            {
                // log error if this was prod and uncomment dex
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

            }


            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,Photo")] Student student, HttpPostedFileBase image)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(student).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(student);

            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength < (256 * 1024) && image.ContentType.Contains("image"))
                {

                    var fileName = Path.GetFileName(image.FileName);
                    var path = Server.MapPath("~/Photos/" + fileName);
                    image.SaveAs(path);
                    student.Photo = "~/Photos/" + fileName;

                    db.Students.Add(student);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                    ViewBag.ImageInvalid = "Please upload a photo of type .gif, .png, .jpeg, .bmp or .webp of less than 256kb.";


            }

            return View(student);
        }

        // GET: Student/Delete/5
        // accept optional parameter to indicate if method was called after a failure to save changes, false initially
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again and if problem persists, see your system administrator.";
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
                
            }
            catch(DataException /*dex*/)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
