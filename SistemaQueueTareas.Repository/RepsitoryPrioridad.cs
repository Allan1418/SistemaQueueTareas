using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Repository
{
    public interface IRepositoryPrioridad: IRepositoryBase<Prioridade>
    {
    }
    public class RepositoryPrioridad: RepositoryBase<Prioridade>, IRepositoryPrioridad
    {
        public RepositoryPrioridad() : base()
        {
        }
    }
}
