using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using UniversityManager.Models;
using UniversityManager.Models.Entity;
using Group = UniversityManager.Entities.Models.Group;

namespace UniversityManager.Areas.Admin.Controllers
{
    public class GroupController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index()
        {
            _context = new ApplicationDbContext();
            return View();
        }

        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGroup(CreateGroupViewModel model)
        {
            Group group = new Group()
            {
                Name = model.Name,
                CountOfStudents = model.CountOfStudents,
                Year = model.Year
            };
            _context.Groups.Add(group);
            _context.SaveChanges();
            return RedirectToAction("DashBoard", "AdminPanel");
        }

        public ActionResult GetAllGroups()
        {
            IEnumerable<GroupViewModel> groups = _context.Groups.Select(x => new GroupViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Year = x.Year,
                CountOfStudents = x.CountOfStudents
            });
            return View(groups);
        }

        public ActionResult Edit(int? id)
        {
            GroupViewModel model = new GroupViewModel();
            model.Name = _context.Groups.Find(id).Name;
            model.Year = _context.Groups.Find(id).Year;
            model.CountOfStudents = _context.Groups.Find(id).CountOfStudents;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(GroupViewModel model)
        {
            Group group = _context.Groups.Find(model.Id);
            group.Name = model.Name;
            group.Year = model.Year;
            group.CountOfStudents = model.CountOfStudents;

            _context.SaveChanges();
            return RedirectToAction("GetAllGroups");
        }

        public ActionResult Delete(int id)
        {
            Group group = _context.Groups.Find(id);
            _context.Groups.Remove(group);
            _context.SaveChanges();
            return RedirectToAction("GetAllGroups");
        }
    }
}