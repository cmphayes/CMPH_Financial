using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMPH_Financial.Startup))]
namespace CMPH_Financial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
