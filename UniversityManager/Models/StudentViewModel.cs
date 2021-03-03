using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManager.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string GroupName { get; set; }
        public string Email { get; set; }
    }
}