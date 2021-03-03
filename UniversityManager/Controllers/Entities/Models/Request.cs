using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManager.Entities.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int StudentId { get; set; }
    }
}