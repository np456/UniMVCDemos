using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment_2.Models;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Imaging;
using System.IO;

namespace Assignment_2.Controllers
{
    //[ControlValueProperty("256")]
    //[ValidationProperty(".jpg")]
    [Authorize(Roles ="Manager")]
    public class studentsController : Controller
    {
        private A2Entities db = new A2Entities();

        // GET: students
        public ActionResult Index()
        {
            return View(db.students.ToList());
        }

        // GET: students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: students/Create
        public ActionResult Create()
        {
            return View();
        }

        /*
        // Save image to database
        [HttpPost]
        public ActionResult AddImage(student model, HttpPostedFileBase image)
        {
            if (image != null )//&& image.ContentLength < (256*1024) && image.ContentType.Contains("image"))
            {
                //model.photo = image
                var fileName = Path.GetFileName(image.FileName);
                var path = Server.MapPath("~/Photos/" + fileName);
                image.SaveAs(path);
                model.photo = "~/Photos/" + fileName;

                db.students.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }*/

        // Validate student id is unique
        [HttpPost]
        public JsonResult IsStudentIDAvailable(int student_id)
        {
            return Json(!db.students.Any(student => student.student_id == student_id), JsonRequestBehavior.AllowGet);

        }

        // POST: students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "student_id,student_first_name,student_last_name,photo")] student student, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength < (256 * 1024) && image.ContentType.Contains("image"))
                {

                    var fileName = Path.GetFileName(image.FileName);
                    var path = Server.MapPath("~/Photos/" + fileName);
                    image.SaveAs(path);
                    student.photo = "~/Photos/" + fileName;

                    db.students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    ViewBag.ImageInvalid = "Please upload a photo of type .gif, .png, .jpeg, .bmp or .webp of less than 256kb.";


            }

            return View(student);
        }

        // GET: students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_id,student_first_name,student_last_name,photo")] student student, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength < (256 * 1024) && image.ContentType.Contains("image"))
                {
                    var fileName = Path.GetFileName(image.FileName);
                    var path = Server.MapPath("~/Photos/" + fileName);
                    image.SaveAs(path);
                    student.photo = "~/Photos/" + fileName;

                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else if (image == null)
                {
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    ViewBag.ImageInvalid = "Please upload a photo of type .gif, .png, .jpeg, .bmp or .webp of less than 256kb.";

            }
            return View(student);
        }

        // GET: students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            student student = db.students.Find(id);
            db.students.Remove(student);
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
