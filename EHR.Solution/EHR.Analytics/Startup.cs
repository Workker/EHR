using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EHR.Analytics.Startup))]
namespace EHR.Analytics
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
