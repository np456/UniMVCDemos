using Assignment_2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment_2.Startup))]
namespace Assignment_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createDefaultUsers();
        }

        private void createDefaultUsers()
        {
            //A2Entities db = new A2Entities();
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup create default admin user  
            if (!roleManager.RoleExists("Admin"))
            {

                // Create new admin role  
                var role = new IdentityRole();
                role.Id = "0";
                role.Name = "Admin";
                roleManager.Create(role);

                // Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "admin@ecu.com";
                user.Email = "admin@ecu.com";
                user.Id = "1";

                string userPWD = "Admin#1";

                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");

                }
            }

            // Create manager role
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Id = "1";
                role.Name = "Manager";
                roleManager.Create(role);

                // Here we create the results manager                

                var user = new ApplicationUser();
                user.UserName = "manager@ecu.com";
                user.Email = "manager@ecu.com";
                user.Id = "2";

                string userPWD = "Manager#1";

                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result2 = userManager.AddToRole(user.Id, "Manager");

                }

            }
        }
    }
}
