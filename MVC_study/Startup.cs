using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_study.Startup))]
namespace MVC_study
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
