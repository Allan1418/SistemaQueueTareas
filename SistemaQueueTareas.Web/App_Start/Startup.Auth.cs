
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Extensions.Hosting;
using Owin;
using SistemaQueueTareas.IdentityData;
using Unity;

[assembly: OwinStartup(typeof(SistemaQueueTareas.Web.Startup))]
namespace SistemaQueueTareas.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configurar dependencias
            var container = DependencyConfig.RegisterDependencies();

            // Configurar autenticación y roles
            ConfigureAuth(app);
            CreateRoles(container);

            // Iniciar servicio en segundo plano
            StartBackgroundService(container);
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }

        private void CreateRoles(IUnityContainer container)
        {
            using (var context = container.Resolve<ApplicationDbContext>())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("user"))
                    roleManager.Create(new IdentityRole("user"));

                if (!roleManager.RoleExists("admin"))
                    roleManager.Create(new IdentityRole("admin"));
            }
        }

        private void StartBackgroundService(IUnityContainer container)
        {
            var service = container.Resolve<IHostedService>();
            service.StartAsync(System.Threading.CancellationToken.None).Wait();
        }
    }
}