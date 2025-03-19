using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Repository
{
    public interface IRepositoryNotificacion : IRepositoryBase<Notificacione>
    {
    }
    public class RepositoryNotificacion : RepositoryBase<Notificacione>, IRepositoryNotificacion
    {
        public RepositoryNotificacion() : base()
        {
        }
    }
}
