using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace SistemaQueueTareas.IdentityData
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("IdentityConnection")
        {
            // Deshabilitar migraciones automaticas e inicializacion de la BD
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
