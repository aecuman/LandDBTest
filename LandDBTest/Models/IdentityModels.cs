using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using static LandDBTest.ApplicationRoleManager;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace LandDBTest.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

       public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public class ApplicationDbInitializer: DropCreateDatabaseIfModelChanges<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                InitializeIdentityForEF(context);
                base.Seed(context);
            }


            //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
            public static void InitializeIdentityForEF(ApplicationDbContext db)
            {
                var userManager =
          HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var roleManager =
                    HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
                const string name = "admin@example.com";
                const string password = "Admin@123456";
                const string roleName = "Admin";
               // var description = roleManager.FindByName(roleName).Description;

                //Create Role Admin if it does not exist
                var role = roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new ApplicationRole(roleName, role.Description);
                    var roleresult = roleManager.Create(role);
                }

                var user = userManager.FindByName(name);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = name, Email = name };
                    var result = userManager.Create(user, password);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }

                // Add user admin to Role Admin if not already added
                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(role.Name))
                {
                    var result = userManager.AddToRole(user.Id, role.Name);
                }
            }
        }
    }


}