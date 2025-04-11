using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaQueueTareas.Data;
using SistemaQueueTareas.Repository;

namespace SistemaQueueTareas.Business
{
    public class StateManager
    {

        RepositoryState _repositoryState;

        public StateManager()
        {
            _repositoryState = new RepositoryState();
        }

        public State StateInProcess( string process)
        { 
            return _repositoryState.ChangeStateInProcess(process);
        }

        
    }
}
