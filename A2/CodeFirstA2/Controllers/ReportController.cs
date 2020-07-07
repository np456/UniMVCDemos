using CodeFirstA2.DataAccessLayer;
using CodeFirstA2.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CodeFirstA2.Controllers
{
    public class ReportController : Controller
    {
        private UniversityContext db = new UniversityContext();
        // GET: Report, displaying the following
        public ActionResult Index(string unitCode, string studentId, string semester, string year, string unitScore)
        {
            // Use queries to sort, filter, page and insert data

            // GET: results
            int count;
            Nullable<int> unitAvg;
            double avg;
            string gradeAvg = "";

            // query once and store to close datareader and allow in memory querying
            var Results = (from m in db.Results
                          select m).ToList();
            


            count = Results.Count();
            ViewBag.Message = "Search Results: " + count;
            unitAvg = Results.Sum(s => s.UnitMark);
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

            // GET: on filter button press
            GetStudentId(studentId, Results);
            GetUnitCode(unitCode, Results);
            GetSemester(semester, Results);
            GetYear(year, Results);
            GetUnitScore(unitScore, Results);

            return View(Results);
        }

        public void GetStudentId(string studentId, List<Result> results)
        {
            var studentIDList = new List<int>();
            var studentIDQuery = from s in db.Results
                                 orderby s.StudentId
                                 select s.StudentId;

            studentIDList.AddRange(studentIDQuery.Distinct());
            ViewBag.studentId = new SelectList(studentIDList);

            int count;
            Nullable<int> unitAvg;
            double avg;
            string gradeAvg = "";

            if (!String.IsNullOrEmpty(studentId))
            {
                // was missing ToList() cast because of LINQ IQUERYABLE
                results = results.Where(s => s.StudentId.ToString() == studentId).ToList();
                count = results.Count();

                unitAvg = results.Sum(s => s.UnitMark);
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

                ViewBag.Message = "Search Results: " + count;
                if (!Double.IsNaN(avg) && !Double.IsInfinity(avg))
                    ViewBag.Message2 = "Unit Avg: " + avg + gradeAvg;
                else
                    ViewBag.Message2 = "Unit Avg: -";
            }
         
        }

        public void GetUnitCode(string unitCode, List<Result> results)
        {

            var unitCodeSearchList = new List<string>();
            var unitCodeQuery = from s in db.Results orderby s.UnitCode select s.UnitCode;
            unitCodeSearchList.AddRange(unitCodeQuery.Distinct());
            ViewBag.unitCode = new SelectList(unitCodeSearchList);

            int count;
            Nullable<int> unitAvg;
            double avg;
            string gradeAvg = "";

            if (!String.IsNullOrEmpty(unitCode))
            {
                results = results.Where(s => s.UnitCode == unitCode).ToList();
                count = results.Count();
                unitAvg = results.Sum(s => s.UnitMark);
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

                ViewBag.Message = "Search Results: " + count;
                if (!Double.IsNaN(avg) && !Double.IsInfinity(avg))
                    ViewBag.Message2 = "Unit Avg: " + avg + gradeAvg;
                else
                    ViewBag.Message2 = "Unit Avg: -";
            }

        }

        public void GetSemester(string semester, List<Result> results)
        {

            var SemesterSearchList = new List<byte>();
            var SemesterSearchQuery = from b in db.Results orderby b.Semester select b.Semester;
            SemesterSearchList.AddRange(SemesterSearchQuery.Distinct());
            ViewBag.semester = new SelectList(SemesterSearchList);

            int count;
            Nullable<int> unitAvg;
            double avg;
            string gradeAvg = "";

            if (!String.IsNullOrEmpty(semester))
            {
                results = results.Where(s => s.Semester.ToString() == semester).ToList();
                count = results.Count();

                unitAvg = results.Sum(s => s.UnitMark);
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

                ViewBag.Message = "Search Results: " + count;
                if (!Double.IsNaN(avg) && !Double.IsInfinity(avg))
                    ViewBag.Message2 = "Unit Avg: " + avg + gradeAvg;
                else
                    ViewBag.Message2 = "Unit Avg: -";
            }

        }

        public void GetYear(string year, List<Result> results)
        {
            var YearSearchList = new List<short>();
            var YearSearchQuery = from s in db.Results orderby s.Year select s.Year;
            YearSearchList.AddRange(YearSearchQuery.Distinct());
            ViewBag.year = new SelectList(YearSearchList);

            int count;
            Nullable<int> unitAvg;
            double avg;
            string gradeAvg = "";

            if (!String.IsNullOrEmpty(year))
            {
                results = results.Where(s => s.Year.ToString() == year).ToList();
                count = results.Count();

                unitAvg = results.Sum(s => s.UnitMark);
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

                ViewBag.Message = "Search Results: " + count;
                if (!Double.IsNaN(avg) && !Double.IsInfinity(avg))
                    ViewBag.Message2 = "Unit Avg: " + avg + gradeAvg;
                else
                    ViewBag.Message2 = "Unit Avg: -";
            }

        }

        public void GetUnitScore(string unitScore, List<Result> results)
        {
            var unitScoreSearchList = new List<Nullable<int>>();
            var unitScoreSearchQuery = from d in db.Results orderby d.UnitMark select d.UnitMark;
            unitScoreSearchList.AddRange(unitScoreSearchQuery.Distinct());
            ViewBag.unitScore = new SelectList(unitScoreSearchList);

            int count;
            Nullable<int> unitAvg;
            double avg;
            string gradeAvg = "";

            if (!String.IsNullOrEmpty(unitScore))
            {
                results = results.Where(s => s.UnitMark.ToString() == unitScore).ToList();
                count = results.Count();

                unitAvg = results.Sum(s => s.UnitMark);
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

                ViewBag.Message = "Search Results: " + count;
                if (!Double.IsNaN(avg) && !Double.IsInfinity(avg))
                    ViewBag.Message2 = "Unit Avg: " + avg + gradeAvg;
                else
                    ViewBag.Message2 = "Unit Avg: -";
            }

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
