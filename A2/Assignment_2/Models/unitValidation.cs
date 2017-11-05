using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Assignment_2.Models
{
    [MetadataType(typeof(unitMetadata))]
    public partial class unit
    {
    }

    public class unitMetadata
    {
        //[Remote("IsUnitCodeAvailable", "units", HttpMethod = "POST", ErrorMessage ="Unit Code already exists.")]
        [Required]
        [Display(Name = "Unit Code")]
        [Remote("IsUnitCodeAvailable", "units", HttpMethod = "POST", ErrorMessage = "Unit Code already exists.")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z][a-zA-Z][0-9][0-9][0-9][0-9]", ErrorMessage = "Unit code must be three letters followed by four numbers.")]
        public string unit_code { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage="Unit title must be only characters.")]
        [Display(Name = "Unit Title")]
        public string unit_title { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "Unit co-ordinator must be only characters.")]
        [Display(Name = "Unit Co-ordinator")]
        public string unit_coordinator { get; set; }
        [Display(Name = "Unit Outline")]
        public string unit_outline_pdf_document { get; set; }
    }
}