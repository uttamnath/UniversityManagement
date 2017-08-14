using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AW_UniversityManagementMvcApp.Startup))]
namespace AW_UniversityManagementMvcApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
