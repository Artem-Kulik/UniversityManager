using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManager.Models.Entity
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "From 2 to 15 symbols")]
        [Display(Name = "Name: ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Students count is required")]
        [Range(8, 100, ErrorMessage = "From 8 to 100")]
        [Display(Name = "Count of students : ")]
        public int CountOfStudents { get; set; }
        [Required(ErrorMessage = "Students count is required")]
        [Range(1, 6, ErrorMessage = "From 1 to 6")]
        [Display(Name = "Year : ")]
        public int Year { get; set; }
    }
}