using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManager.Entities.Models;
using UniversityManager.Helpers;
using UniversityManager.Models;
using Constants = UniversityManager.Helpers.Constants;

namespace UniversityManager.Areas.Admin.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
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
            return RedirectToAction("DashBoard", "AdminPanel");
        }

        public ActionResult Delete(int id)
        {
            Student st = _context.Students.Find(id);
            ApplicationUser ap = st.ApplicationUser;

            _context.Students.Remove(st);
            _context.Users.Remove(ap);

            _context.SaveChanges();
            return RedirectToAction("GetAllGroups", "AdminPanel");
        }

        public ActionResult GetAllStudents()
        {
            IEnumerable<StudentViewModel> students = _context.Students.Select(x => new StudentViewModel
            {
                Id = x.Id,
                Email = x.ApplicationUser.Email,
                GroupName = x.Group.Name,
                Image = x.Image,
                Age = x.Age,
                Phone = x.Phone
            });
            return View(students);
        }
    }
}