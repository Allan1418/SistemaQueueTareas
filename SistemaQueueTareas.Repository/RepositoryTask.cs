using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Repository
{
    public interface IRepositoryTask : IRepositoryBase<Task>
    {
        List<Task> GetTasksByUserAndFilters(string userId, int? stateId, int? priorityId);
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

        public List<Task> findTaskByState(int id_state)
        {
            return _dbSet.Where(t => t.id_state == id_state)
                         .Include(t => t.Priority)
                         .Include(t => t.State)
                         .ToList();
        }

        public List<Task> findTaskByState(string taskState)
        {
            return _dbSet.Where(t => t.State.name == taskState).ToList();
        }

        public Task GetById(int id, string userId)
        {
            return _dbSet.FirstOrDefault(t => t.id == id && t.id_user == userId);
        }

        public List<Task> findTaskByUser(string id_user)
        {
            return _dbSet.Where(t => t.id_user == id_user)
                         .Include(t => t.Priority)
                         .Include(t => t.State)
                         .ToList();
        }
        public List<Task> GetTasksByUserAndFilters(string userId, int? stateId, int? priorityId)
        {
            var query = _dbSet.Where(t => t.id_user == userId)
                              .Include(t => t.Priority)
                              .Include(t => t.State);

            if (stateId.HasValue)
                query = query.Where(t => t.id_state == stateId.Value);

            if (priorityId.HasValue)
                query = query.Where(t => t.id_priority == priorityId.Value);

            return query.ToList();
        }
    }
}