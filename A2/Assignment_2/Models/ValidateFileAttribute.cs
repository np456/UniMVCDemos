using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace Assignment_2.Models
{
    public class ValidateFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            int maxFileSize = 256 * 1024; //256kb
            string[] AllowdedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

            if (file == null)
            {
                return false;
            }
            /*if (file.ContentLength > maxFileSize)
            {
                
                return false;
            }
            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    return img.RawFormat.Equals(ImageFormat.Png);
                }
            }
            catch { }
            return false;*/
            else if (!AllowdedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = "Please upload a file of type: " + string.Join(", ", AllowdedFileExtensions);
                return false;
            }
            else if (file.ContentLength > maxFileSize)
            {
                ErrorMessage = "File uploaded exceeds maximum allowded size of: " + (maxFileSize).ToString() + "kb";
                return false;
            }
            else
                return true;
            
            /*try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    return img.RawFormat.Equals(ImageFormat.Png);
                }
            }
            catch { }
            return false;*/
        }
    }
}