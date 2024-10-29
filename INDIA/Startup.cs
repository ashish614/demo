using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(INDIA.Startup))]
namespace INDIA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
