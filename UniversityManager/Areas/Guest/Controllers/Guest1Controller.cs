using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManager.Entities.Models;
using UniversityManager.Helpers;
using UniversityManager.Models;
using UniversityManager.Models.Entity;
using Constants = UniversityManager.Helpers.Constants;

namespace UniversityManager.Areas.Guest.Controllers
{
    public class Guest1Controller : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyInfo()
        {
            var res = _context.Users.Find(User.Identity.GetUserId()).Student;
            ViewBag.ImagePath = res.Image;
            StudentViewModel st = new StudentViewModel()
            {
                Age = res.Age,
                Email = res.ApplicationUser.Email,
                GroupName = res.Group != null ? res.Group.Name : "",
                Phone = res.Phone,
                Image = res.Image,
                Id = res.Id
            };

            string status = "";

            foreach (var item in _context.Statuses)
            {
                if (item.StudentId == res.Id)
                {
                    status = item.StatusInfo;
                    break;
                }
            }
            if (status != "") ViewBag.Status = status;
            return View(st);
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

        public ActionResult PullRequest(int id)
        {
            Request r = new Request();
            r.GroupId = id;
            r.StudentId = _context.Users.Find(User.Identity.GetUserId()).Student.Id;

            Student student = _context.Users.Find(User.Identity.GetUserId()).Student;
            var status = _context.Statuses.Where(el => el.StudentId == student.Id).FirstOrDefault();
            if (status != null)
            {
                if (status.StatusInfo != "Waiting for answer")
                {
                    _context.Requests.Add(r);

                    Status st = new Status()
                    {
                        StatusInfo = "Waiting for answer",
                        StudentId = r.StudentId
                    };

                    _context.Statuses.Add(st);
                    _context.SaveChanges();
                }
            }
            else
            {
                _context.Requests.Add(r);

                Status st = new Status()
                {
                    StatusInfo = "Waiting for answer",
                    StudentId = r.StudentId
                };

                _context.Statuses.Add(st);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Guest1");
        }

        public ActionResult Edit(int id)
        {
            StudentViewModel model = new StudentViewModel();
            var el = _context.Students.Find(id);
            model.Email = el.ApplicationUser.Email;
            model.Image = el.Image;
            model.Age = el.Age;
            model.Phone = el.Phone;
            model.Id = el.Id;

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(StudentViewModel el, HttpPostedFileBase file)
        {
            Student st = _context.Students.Find(el.Id);
            st.ApplicationUser.Email = el.Email;
            st.Age = el.Age;
            st.Phone = el.Phone;
            st.Id = el.Id;

            st.Image = el.Image;

            if (file != null)
            {
                string filename = Guid.NewGuid().ToString() + ".jpg";
                string image = Server.MapPath(Constants.ImagePath) + "\\" + filename;

                using (Bitmap bmp = new Bitmap(file.InputStream))
                {
                    Bitmap saveImage = ImageWorker.CreateImage(bmp, 200, 120);
                    if (saveImage != null)
                    {
                        saveImage.Save(image);
                        st.Image = filename;
                    }
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}