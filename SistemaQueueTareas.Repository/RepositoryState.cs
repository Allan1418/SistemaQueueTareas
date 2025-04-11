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
        State GetStateByName(string name);
    }
    public class RepositoryState : RepositoryBase<State>, IRepositoryState
    {
        public RepositoryState() : base()
        {
        }
        public State GetStateByName(string name)
        {
            return _dbSet.FirstOrDefault(s => s.name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public State ChangeStateInProcess(string nameState)
        {
            return _context.States.FirstOrDefault(s => s.name == nameState);
        }

        
    }
}
