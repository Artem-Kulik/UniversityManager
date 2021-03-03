using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManager.Models
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Email { get; set; }
    }
}