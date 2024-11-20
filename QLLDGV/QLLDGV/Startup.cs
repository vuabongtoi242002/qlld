using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLLDGV.Startup))]
namespace QLLDGV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
