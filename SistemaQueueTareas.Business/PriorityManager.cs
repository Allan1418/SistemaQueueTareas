using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaQueueTareas.Data;
using SistemaQueueTareas.Repository;

namespace SistemaQueueTareas.Business
{
    public class PriorityManager
    {
        private RepositoryPriority _repositoryPriority;
        public PriorityManager() 
        {
            _repositoryPriority = new RepositoryPriority();
        }


        public List<Priority> GetAllOrderPriorities()
        {
            return _repositoryPriority.GetAllOrderPriorities();
        }
    }
}
