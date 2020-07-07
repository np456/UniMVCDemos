using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Assignment_2.Models
{
    [MetadataType(typeof(resultMetadata))]
    public partial class result
    {
    }

    public class resultMetadata
    {
        [Required]
        [Display(Name = "Semester")]
        [RegularExpression(@"^[1-2]$", ErrorMessage = "Semester must be 1 or 2")]
        public byte semester { get; set; }
        [Required]
        [Display(Name = "Year")]
        [RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "Year must be between 1900 - 2099")]
        public short year { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]|1[0-9]|20)$", ErrorMessage = "Assessment 1 mark must be between 0 and 20")]
        [Display(Name = "Assessment 1")]
        public int ass_1 { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]|1[0-9]|20)$", ErrorMessage = "Assessment 2 mark must be between 0 and 20")]
        [Display(Name = "Assessment 2")]
        public int ass_2 { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]|[1-5][0-9]|60)$", ErrorMessage = "Exam mark must be between 0 and 60")]
        [Display(Name = "Exam")]
        public int exam { get; set; }
        [Display(Name = "Unit Mark")]
        public Nullable<int> unit_mark { get; set; }
        [Display(Name = "Grade")]
        public string grade { get; set; }
    }
}