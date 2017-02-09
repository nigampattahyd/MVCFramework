using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRM.Startup))]
namespace CRM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
