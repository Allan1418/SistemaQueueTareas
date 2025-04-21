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
        List<Priority> GetAllOrderPriorities();
    }
    public class RepositoryPriority: RepositoryBase<Priority>, IRepositoryPriority
    {
        public RepositoryPriority() : base()
        {
        }


        //Show priority in a logic order "High>Medium>Low" 
        public List<Priority> GetAllOrderPriorities()
        {
            return _context.Priorities.OrderBy(p => p.order).ToList();
        }
    }
}
