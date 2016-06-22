using LandDBTest.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(LandDBTest.Startup))]
namespace LandDBTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
            validateInterval: System.TimeSpan.FromMinutes(15),
            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager)),
                },
                SlidingExpiration = false,
                ExpireTimeSpan = TimeSpan.FromMinutes(30)
            });
            ConfigureAuth(app);
        }
    }
}
