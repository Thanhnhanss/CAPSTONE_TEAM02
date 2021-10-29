using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VanLangDoctor.Startup))]
namespace VanLangDoctor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
