using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LandDBTest.Startup))]
namespace LandDBTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
