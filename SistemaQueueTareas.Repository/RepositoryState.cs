using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Repository
{
    public interface IRepositoryState : IRepositoryBase<State>
    {
    }
    public class RepositoryState : RepositoryBase<State>, IRepositoryState
    {
        public RepositoryState() : base()
        {
        }
    }
}
