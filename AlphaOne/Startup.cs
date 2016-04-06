using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AlphaOne.Startup))]
namespace AlphaOne
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
