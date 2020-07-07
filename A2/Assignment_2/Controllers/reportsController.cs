using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment_2.Models;

namespace Assignment_2.Controllers
{
    [Authorize(Roles ="Manager")]
    public class reportsController : Controller
    {
        private A2Entities db = new A2Entities();

      /*  // GET: reports
        public ActionResult Index()
        {
            var results = db.results.Include(r => r.student).Include(r => r.unit);
            return View(results.ToList());
        }   */

        // search
        public ActionResult Index(string unitCodeSearch, string studentIDSearch, string semesterSearch, string yearSearch, string unitScoreSearch)
        {
            var studentIDList = new List<int>();
            var studentIDQuery = from d in db.results
                                 orderby d.student_id
                                 select d.student_id;

            studentIDList.AddRange(studentIDQuery.Distinct());
            ViewBag.studentIDSearch = new SelectList(studentIDList);

            var unitCodeSearchList = new List<string>();
            var unitCodeSearchQuery = from s in db.results orderby s.unit_code select s.unit_code;
            unitCodeSearchList.AddRange(unitCodeSearchQuery.Distinct());
            ViewBag.unitCodeSearch = new SelectList(unitCodeSearchList);

            var semesterSearchList = new List<byte>();
            var semesterSearchQuery = from b in db.results orderby b.semester select b.semester;
            semesterSearchList.AddRange(semesterSearchQuery.Distinct());
            ViewBag.semesterSearch = new SelectList(semesterSearchList);

            var yearSearchList = new List<short>();
            var yearSearchQuery = from s in db.results orderby s.year select s.year;
            yearSearchList.AddRange(yearSearchQuery.Distinct());
            ViewBag.yearSearch = new SelectList(yearSearchList);

            var unitScoreSearchList = new List<Nullable<int>>();
            var unitScoreSearchQuery = from d in db.results orderby d.unit_mark select d.unit_mark;
            unitScoreSearchList.AddRange(unitScoreSearchQuery.Distinct());
            ViewBag.unitScoreSearch = new SelectList(unitScoreSearchList);

            int count;
            Nullable<int> unitAvg;
            double avg;
            string gradeAvg = "";
            
           /* switch(gradeAvg)
            {
                case "F":
                    if(avg <= 49)
                        gradeAvg = "F";
                    break;
                case "P":
                    if (avg <= 59)
                        gradeAvg = "P";
                    break;
                case "C":
                    if (avg <= 69)
                        gradeAvg = "C";
                    break;
                case "D":
                    if (avg <= 79)
                        gradeAvg = "D";
                    break;
                case "HD":
                    if (avg <= 100)
                        gradeAvg = "HD";
                    break;
                default:
                    break;
            }*/

            var results = from m in db.results
                         select m;

            count = results.Count();
            ViewBag.Message = "Search results: " + count;
            unitAvg = results.Sum(s => s.unit_mark);
            avg = Math.Round((Convert.ToDouble(unitAvg) / (100.00 * count)) * 100.00, 2);

            if (avg <= 49 && avg >= 0)
                gradeAvg = "F";
            if (avg >= 50 && avg <= 59)
                gradeAvg = "P";
            if (avg >= 60 && avg <= 69)
                gradeAvg = "C";
            if (avg >= 70 && avg <= 79)
                gradeAvg = "D";
            if (avg >= 80 && avg <= 100)
                gradeAvg = "HD";

            if (!Double.IsNaN(avg) && !Double.IsInfinity(avg))
                ViewBag.Message2 = "Unit Avg: " + avg + gradeAvg;
            else
                ViewBag.Message2 = "Unit Avg: -";
            //ViewBag.Message2 = "Unit Avg: " + results.Sum(unit_mark)
            /*if (!String.IsNullOrEmpty(unitSearch))    // textbox search
            {
                results = results.Where(s => s.unit_code.Contains(unitSearch));
            }*/
            if (!String.IsNullOrEmpty(unitCodeSearch))
            {
                results = results.Where(s => s.unit_code == unitCodeSearch);
                count = results.Count();
                unitAvg = results.Sum(s => s.unit_mark);
                avg = Math.Round((Convert.ToDouble(unitAvg) / (100.00 * count)) * 100.00, 2);

                if (avg <= 49 && avg >= 0)
                    gradeAvg = "F";
                if (avg >= 50 && avg <= 59)
                    gradeAvg = "P";
                if (avg >= 60 && avg <= 69)
                    gradeAvg = "C";
                if (avg >= 70 && avg <= 79)
                    gradeAvg = "D";
                if (avg >= 80 && avg <= 100)
                    gradeAvg = "HD";

                ViewBag.Message = "Search results: " + count;
                if (!Double.IsNaN(avg) && !Double.IsInfinity(avg))
                    ViewBag.Message2 = "Unit Avg: " + avg + gradeAvg;
                else
                    ViewBag.Message2 = "Unit Avg: -";
            }


            if (!String.IsNullOrEmpty(studentIDSearch))
            {
                results = results.Where(s => s.student_id.ToString() == studentIDSearch);
                count = results.Count();

                unitAvg = results.Sum(s => s.unit_mark);
                avg = Math.Round((Convert.ToDouble(unitAvg) / (100.00 * count)) * 100.00, 2);

                if (avg <= 49 && avg >= 0)
                    gradeAvg = "F";
                if (avg >= 50 && avg <= 59)
                    gradeAvg = "P";
                if (avg >= 60 && avg <= 69)
                    gradeAvg = "C";
                if (avg >= 70 && avg <= 79)
                    gradeAvg = "D";
                if (avg >= 80 && avg <= 100)
                    gradeAvg = "HD";

                ViewBag.Message = "Search results: " + count;
                if (!Double.IsNaN(avg) && !Double.IsInfinity(avg))
                    ViewBag.Message2 = "Unit Avg: " + avg + gradeAvg;
                else
                    ViewBag.Message2 = "Unit Avg: -";
            }

            if (!String.IsNullOrEmpty(semesterSearch))
            {
                results = results.Where(s => s.semester.ToString() == semesterSearch);
                count = results.Count();

                unitAvg = results.Sum(s => s.unit_mark);
                avg = Math.Round((Convert.ToDouble(unitAvg) / (100.00 * count)) * 100.00, 2);

                if (avg <= 49 && avg >= 0)
                    gradeAvg = "F";
                if (avg >= 50 && avg <= 59)
                    gradeAvg = "P";
                if (avg >= 60 && avg <= 69)
                    gradeAvg = "C";
                if (avg >= 70 && avg <= 79)
                    gradeAvg = "D";
                if (avg >= 80 && avg <= 100)
                    gradeAvg = "HD";

                ViewBag.Message = "Search results: " + count;
                if(!Double.IsNaN(avg) && !Double.IsInfinity(avg))
                ViewBag.Message2 = "Unit Avg: " + avg + gradeAvg;
                else
                    ViewBag.Message2 = "Unit Avg: -";
            }

            if (!String.IsNullOrEmpty(yearSearch))
            {
                results = results.Where(s => s.year.ToString() == yearSearch);
                count = results.Count();

                unitAvg = results.Sum(s => s.unit_mark);
                avg = Math.Round((Convert.ToDouble(unitAvg) / (100.00 * count)) * 100.00, 2);

                if (avg <= 49 && avg >= 0)
                    gradeAvg = "F";
                if (avg >= 50 && avg <= 59)
                    gradeAvg = "P";
                if (avg >= 60 && avg <= 69)
                    gradeAvg = "C";
                if (avg >= 70 && avg <= 79)
                    gradeAvg = "D";
                if (avg >= 80 && avg <= 100)
                    gradeAvg = "HD";

                ViewBag.Message = "Search results: " + count;
                if (!Double.IsNaN(avg) && !Double.IsInfinity(avg))
                    ViewBag.Message2 = "Unit Avg: " + avg + gradeAvg;
                else
                    ViewBag.Message2 = "Unit Avg: -";
            }

            if (!String.IsNullOrEmpty(unitScoreSearch))
            {
                results = results.Where(s => s.unit_mark.ToString() == unitScoreSearch);
                count = results.Count();

                unitAvg = results.Sum(s => s.unit_mark);
                avg = Math.Round((Convert.ToDouble(unitAvg) / (100.00 * count)) * 100.00, 2);

                if (avg <= 49 && avg >= 0)
                    gradeAvg = "F";
                if (avg >= 50 && avg <= 59)
                    gradeAvg = "P";
                if (avg >= 60 && avg <= 69)
                    gradeAvg = "C";
                if (avg >= 70 && avg <= 79)
                    gradeAvg = "D";
                if (avg >= 80 && avg <= 100)
                    gradeAvg = "HD";

                ViewBag.Message = "Search results: " + count;
                if (!Double.IsNaN(avg) && !Double.IsInfinity(avg))
                    ViewBag.Message2 = "Unit Avg: " + avg + gradeAvg;
                else
                    ViewBag.Message2 = "Unit Avg: -";
            }


            return View(results);
        }

        // GET: reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            result result = db.results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: reports/Create
        public ActionResult Create()
        {
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_first_name");
            ViewBag.unit_code = new SelectList(db.units, "unit_code", "unit_title");
            return View();
        }

        // POST: reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "result_id,student_id,unit_code,semester,year,ass_1,ass_2,exam,unit_mark,grade")] result result)
        {
            if (ModelState.IsValid)
            {
                db.results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.student_id = new SelectList(db.students, "student_id", "student_first_name", result.student_id);
            ViewBag.unit_code = new SelectList(db.units, "unit_code", "unit_title", result.unit_code);
            return View(result);
        }

        // GET: reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            result result = db.results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_first_name", result.student_id);
            ViewBag.unit_code = new SelectList(db.units, "unit_code", "unit_title", result.unit_code);
            return View(result);
        }

        // POST: reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "result_id,student_id,unit_code,semester,year,ass_1,ass_2,exam,unit_mark,grade")] result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.student_id = new SelectList(db.students, "student_id", "student_first_name", result.student_id);
            ViewBag.unit_code = new SelectList(db.units, "unit_code", "unit_title", result.unit_code);
            return View(result);
        }

        // GET: reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            result result = db.results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            result result = db.results.Find(id);
            db.results.Remove(result);
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
