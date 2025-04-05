using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Repository
{
    public interface IRepositoryTask: IRepositoryBase<Task>
    {
    }
    public class RepositoryTask: RepositoryBase<Task>, IRepositoryTask
    {
        public RepositoryTask() : base()
        {
        }

        public List<Task> findAllTask()
        {
            return _dbSet.Include(t => t.Priority)
                         .Include(t => t.State)
                         .ToList();
        }

        public List<Task> findTaskByUser(string id_user)
        {
            return _dbSet.Where(t => t.id_user == id_user)
                         .Include(t => t.Priority)
                         .Include(t => t.State)
                         .ToList();
        }

        public List<Task> findTaskByState(string taskState)
        {
            return _dbSet.Where(t => t.State.name == taskState).ToList();
        }
    }
}