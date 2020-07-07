using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeFirstA2.Models
{
    public enum Grade
    { 
        F, P, C, D, HD
    }

    public class Result
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ResultId { get; set; }
        public string UnitCode { get; set; }
        public int StudentId { get; set; }
        public byte Semester { get; set; }
        public short Year { get; set; }
        public int Ass_1 { get; set; }
        public int Ass_2 { get; set; }
        public int Exam { get; set; }
        public Nullable<int> UnitMark { get; set; }
        public Grade? Grade { get; set; }

        public virtual Student Student { get; set; }
        public virtual Unit Unit { get; set; }

    }
}