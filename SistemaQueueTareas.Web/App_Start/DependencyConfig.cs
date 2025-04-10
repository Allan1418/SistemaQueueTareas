using SistemaQueueTareas.Business;
using SistemaQueueTareas.Data;
using SistemaQueueTareas.IdentityData;
using SistemaQueueTareas.Repository;
using Microsoft.Extensions.Hosting;
using Unity;
using Unity.Lifetime;

namespace SistemaQueueTareas.Web
{
    public static class DependencyConfig
    {
        public static IUnityContainer RegisterDependencies()
        {
            var container = new UnityContainer();

            // Registrar contexto de la base de datos
            container.RegisterType<ApplicationDbContext>(new HierarchicalLifetimeManager());

            // Registrar repositorios
            container.RegisterType<RepositoryTask>(new ContainerControlledLifetimeManager());
            container.RegisterType<RepositoryState>(new ContainerControlledLifetimeManager());
            container.RegisterType<RepositoryPriority>(new ContainerControlledLifetimeManager());
            container.RegisterType<RepositoryNotification>(new ContainerControlledLifetimeManager());

            // Registrar cola de prioridades
            var priorityRepo = container.Resolve<RepositoryPriority>();
            var priorities = priorityRepo.GetAll(); // Método que obtiene todas las prioridades
            container.RegisterInstance(new PriorityTaskQueue(priorities));

            // Registrar servicio en segundo plano
            container.RegisterType<IHostedService, TaskProcessorService>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}