using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManager.Models;

namespace UniversityManager.Helpers
{
    public class SeederDatabase
    {
        public static void SeedData()
        {
            var context = new ApplicationDbContext();
            SeedUsers(context);
        }

        private static void SeedUsers(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.Roles.Any())
            {
                IdentityRole roleAdmin = new IdentityRole()
                {
                    Name = "Admin"
                };
                IdentityRole roleUser = new IdentityRole()
                {
                    Name = "User"
                };
                IdentityRole roleGuest = new IdentityRole()
                {
                    Name = "Guest"
                };
                roleManager.Create(roleAdmin);
                roleManager.Create(roleUser);
                roleManager.Create(roleGuest);
            }
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            if (!userManager.Users.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };
                userManager.Create(user, "Qwerty1-");
                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}