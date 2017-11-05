using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Assignment_2.Models
{
    [MetadataType(typeof(studentMetadata))]
    public partial class student
    {
        //[NotMapped,ValidateFile]
        //public HttpPostedFileBase image { get; set; }
    }

    public class studentMetadata
    {
        [Required]
        [Display(Name = "Student ID")]
        [Remote("IsStudentIDAvailable", "students", HttpMethod = "POST", ErrorMessage = "Student ID already exists.")]
        [RegularExpression(@"^[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]", ErrorMessage = "Student ID must eight digits.")]
        public int student_id { get; set; }
        [Required]
        [Display(Name = "First name")]
        [RegularExpression(@"^[a-zA-Z]{1,50}$", ErrorMessage = "First name must be characters only.")]
        public string student_first_name { get; set; }
        [Required]
        [Display(Name = "Last name")]
        [RegularExpression(@"^[a-zA-Z]{1,50}$", ErrorMessage = "Last name must be characters only.")]
        public string student_last_name { get; set; }
        [Display(Name = "Photo")]
        //[ValidateFile(ErrorMessage = "Please select a file of type .pdf, .jpg, .gif, .png less than 256kb.")]
        //[Remote("AddImage", "students", HttpMethod ="POST", ErrorMessage ="File must be less than 256kb and .jpg")]
        //[NotMapped, ValidateFile]
        public string photo { get; set; }
    }
}