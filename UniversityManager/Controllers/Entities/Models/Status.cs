using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManager.Entities.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string StatusInfo { get; set; }
        public int StudentId { get; set; }
    }
}