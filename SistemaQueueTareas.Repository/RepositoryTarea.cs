using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Repository
{
    public interface IRepositoryTarea: IRepositoryBase<Tarea>
    {
    }
    public class RepositoryTarea: RepositoryBase<Tarea>, IRepositoryTarea
    {
        public RepositoryTarea() : base()
        {
        }
    }
}
