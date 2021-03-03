using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Web;
using System.Web.Mvc;
using UniversityManager.Entities.Models;
using UniversityManager.Models;
using UniversityManager.Models.Entity;
using Group = UniversityManager.Entities.Models.Group;

namespace UniversityManager.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        //Створити додаток "University manager"
        //В ньому реалізувати 3 ролі:
        //• Administrator
        //• User
        //• Guest
        //Для відповідних ролей за потреби розробити areas.
        //Функціонал незалогіненого юзера:
        //• Переглядати домашню сторінку про університет
        //• Можливість зареєструватися
        //Функціонал Guest:
        //• Перегляд свого профілю
        //• Можливість редагувати дані профілю
        //• Перегляд списку груп, до яких він може приєднатися
        //• Надіслати запит на приєднання до групи
        //Функціонал User:
        //• Перегляд інформації про групу, в якій він знаходиться(включаючи список одногрупників)
        //• Кинути запит на приєднання до іншої групи(необов'язково виконувати, за бажанням)
        //Функціонал Administrator:
        //• СRUD по групам
        //• СRUD по студентам (все, окрім створення)
        //• Можливість приймати запит на приєднання до групи

        public ActionResult DashBoard()
        {
            _context = new ApplicationDbContext();
            ViewBag.CountAdmin = _context.Users.Where(el=>el.LockoutEnabled == false).Count();
            ViewBag.CountUser = _context.Users.Where(el=>el.LockoutEnabled == true).Count();
            ViewBag.CountGroups = _context.Groups.Count();
            ViewBag.CountRequests = _context.Requests.Count();
            return View();
        }
        public ActionResult GetRequests()
        {
            List<RequestViewModel> res = new List<RequestViewModel>();
            var requests = _context.Requests;
            foreach (var el in requests)
            {
                try
                {
                    RequestViewModel r = new RequestViewModel();
                    var group = _context.Groups.Find(el.GroupId);

                    r.GroupName = group.Name;

                    var student = _context.Students.Find(el.StudentId);
                    r.Email = student.ApplicationUser.Email;

                    res.Add(r);
                }
                catch (Exception ex) { var ggg = ex.InnerException; };                
            }
            return View(res/*_context.Requests*/);
        }

        public ActionResult Accept(string id)
        {
            //var req = _context.Requests.Find(id);
            var g = _context.Groups.Where(el => el.Name == id).FirstOrDefault();
            var req = _context.Requests.Where(el => el.GroupId == g.Id).FirstOrDefault();

            var group = _context.Groups.Find(req.GroupId);
            _context.Students.Find(req.StudentId).Group = group;

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
            var userId = _context.Students.Find(req.StudentId).ApplicationUser.Id;

            _context.Statuses.Where(el => el.StudentId == req.StudentId).First().StatusInfo = "You were added to the group";

            userManager.RemoveFromRole(userId, "Guest");
            userManager.AddToRole(userId, "User");

            _context.Requests.Remove(req);
            _context.SaveChanges();

            return RedirectToAction("DashBoard", "AdminPanel");
        }

        public ActionResult Cancel(string id)
        {
            var g = _context.Groups.Where(el => el.Name == id).FirstOrDefault();
            var req = _context.Requests.Where(el => el.GroupId == g.Id).FirstOrDefault();

            _context.Statuses.Where(el => el.StudentId == req.StudentId).First().StatusInfo = "You couldn't change the group";

            _context.Requests.Remove(req);
            _context.SaveChanges();
            return RedirectToAction("DashBoard", "AdminPanel");
        }
    }
}