using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Repository
{
    public interface IRepositoryHistorialEjecucion: IRepositoryBase<Historial_Ejecucion>
    {
    }
    public class RepositoryHistorial_Ejecucion: RepositoryBase<Historial_Ejecucion>, IRepositoryHistorialEjecucion
    {
        public RepositoryHistorial_Ejecucion() : base()
        {
        }
    }
}
