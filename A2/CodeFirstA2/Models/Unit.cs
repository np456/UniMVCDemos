using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeFirstA2.Models
{
    public class Unit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string UnitCode { get; set; }
        public string UnitTitle { get; set; }
        public string UnitCoordinator { get; set; }
        public string UnitOutlinePDFPath { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}