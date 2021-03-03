using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UniversityManager.Models;

namespace UniversityManager.Entities.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }

        public int Age { get; set; }

        [Phone]
        public string Phone { get; set; }

        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}