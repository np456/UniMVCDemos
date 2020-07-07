
using CodeFirstA2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirstA2.DataAccessLayer
{
    public class UniversityInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<UniversityContext>
    {
        protected override void Seed(UniversityContext context)
        {
            //base.Seed(context);
            var students = new List<Student>
            {
                new Student{StudentId=99573784, FirstName="Blinky", LastName="Bill", Photo="~/Photos/koala.jpeg" }

            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();


            var units = new List<Unit>
            {
                new Unit{UnitCode="SCI1125", UnitTitle="Professional Science Essentials", UnitCoordinator="Dr Anna Jenny Hopkins", UnitOutlinePDFPath= "~/UnitOutlines/302269 Risk Management 421 Semester 1 2013 Bentley Campus INT.pdf"}
            };

            units.ForEach(u => context.Units.Add(u));
            context.SaveChanges();

            var results = new List<Result>
            {
                new Result{StudentId=99573784, UnitCode="SCI1125", Semester=1, Year=1991, Ass_1=20, Ass_2=20, Exam=30, Grade=Grade.D, UnitMark= 70}
            };

            results.ForEach(r => context.Results.Add(r));
            context.SaveChanges();
        }

    }
}