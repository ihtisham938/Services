using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineHomeServices.Startup))]
namespace OnlineHomeServices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
