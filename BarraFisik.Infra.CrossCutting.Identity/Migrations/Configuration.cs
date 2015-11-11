using System.Data.Entity.Migrations;
using System.Linq;
using BarraFisik.Infra.CrossCutting.Identity.Context;
using BarraFisik.Infra.CrossCutting.Identity.Infra;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BarraFisik.Infra.CrossCutting.Identity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AspNetIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AspNetIdentityDbContext context)
        {

            if (!context.Users.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                // Add missing roles
                //var role = roleManager.FindByName("Admin");
                //if (role == null)
                //{
                //    role = new IdentityRole("Admin");
                //    roleManager.Create(role);
                //}
                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole { Name = "Admin" });
                }

                // Create test users
                var u = userManager.FindByName("admin");
                if (u == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = "admin",
                        Email = "admin@admin.com",
                        EmailConfirmed = true,
                        Name = "admin"
                    };
                    userManager.Create(newUser, "adminadmin");
                    userManager.SetLockoutEnabled(newUser.Id, false);
                    userManager.AddToRole(newUser.Id, "Admin");
                }
            }
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());

            ////var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AspNetIdentityDbContext()));

            ////var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AspNetIdentityDbContext()));        

            //var user = new ApplicationUser
            //{
            //    UserName = "admin",
            //    Email = "admin@admin.com",
            //    EmailConfirmed = true,
            //    Name = "admin"
            //};

            //manager.Create(user, "admin");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //}

            //var adminUser = manager.FindByName("admin");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin" });
        }
    }
}