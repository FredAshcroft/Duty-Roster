using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DutyRoster.Startup))]
namespace DutyRoster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        } 
    }
}
