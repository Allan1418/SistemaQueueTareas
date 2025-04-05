using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Repository
{
    public interface IRepositoryPriority: IRepositoryBase<Priority>
    {
    }
    public class RepositoryPriority: RepositoryBase<Priority>, IRepositoryPriority
    {
        public RepositoryPriority() : base()
        {
        }
    }
}
