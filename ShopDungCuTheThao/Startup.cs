using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System.Web.Services.Description;

[assembly: OwinStartup(typeof(ShopDungCuTheThao.StartupOwin))]

namespace ShopDungCuTheThao
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);

        }
    }
}
