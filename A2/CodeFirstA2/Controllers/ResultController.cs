﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeFirstA2.DataAccessLayer;
using CodeFirstA2.Models;

namespace CodeFirstA2.Controllers
{
    public class ResultController : Controller
    {
        private UniversityContext db = new UniversityContext();

        // GET: Result
        public ActionResult Index()
        {
            var results = db.Results.Include(r => r.Student).Include(r => r.Unit);
            return View(results.ToList());
        }

        // GET: Result/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Result/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName");
            ViewBag.UnitCode = new SelectList(db.Units, "UnitCode", "UnitTitle");
            return View();
        }

        // POST: Result/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResultId,UnitCode,StudentId,Semester,Year,Ass_1,Ass_2,Exam,UnitMark,Grade")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", result.StudentId);
            ViewBag.UnitCode = new SelectList(db.Units, "UnitCode", "UnitTitle", result.UnitCode);
            return View(result);
        }

        // GET: Result/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", result.StudentId);
            ViewBag.UnitCode = new SelectList(db.Units, "UnitCode", "UnitTitle", result.UnitCode);
            return View(result);
        }

        // POST: Result/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResultId,UnitCode,StudentId,Semester,Year,Ass_1,Ass_2,Exam,UnitMark,Grade")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", result.StudentId);
            ViewBag.UnitCode = new SelectList(db.Units, "UnitCode", "UnitTitle", result.UnitCode);
            return View(result);
        }

        // GET: Result/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
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
