using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Repository
{
    public interface IRepositoryPrioridad: IRepositoryBase<Priority>
    {
    }
    public class RepositoryPrioridad: RepositoryBase<Priority>, IRepositoryPrioridad
    {
        public RepositoryPrioridad() : base()
        {
        }
    }
}
