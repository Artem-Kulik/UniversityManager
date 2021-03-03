using Microsoft.Owin;
using Owin;
using UniversityManager.Helpers;

[assembly: OwinStartupAttribute(typeof(UniversityManager.Startup))]
namespace UniversityManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            SeederDatabase.SeedData();
        }
    }
}
