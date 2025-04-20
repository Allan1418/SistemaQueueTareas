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
        //Interface Segregation Principle
        //Single Responsibility Principle
        List<Task> FindTaskByUser(string id_user);
        List<Task> FindTaskByState(int id_state);
        List<Task> FindTaskByState(string taskState);
        Task GetUserTaskById(int taskId, string userId);
        IEnumerable<Task> GetPendingTasks();
        List<Task> OrderByPriorities(string userId, int? id_state, int? id_priority);


    }
    public class RepositoryTask : RepositoryBase<Task>, IRepositoryTask
    {
        public RepositoryTask() : base()
        {
        }

        public IEnumerable<Task> GetPendingTasks()
        {
            return _context.Tasks
                .Include(t => t.Priority)
                .Where(t => t.State.name == "Pendiente")//expression lambda, Open/Close principle
                .ToList();
        }

        public List<Task> FindTaskByState(int id_state)
        {
            return _dbSet.Where(t => t.id_state == id_state)
                         .Include(t => t.Priority)
                         .Include(t => t.State)
                         .ToList();
        }

        public List<Task> FindTaskByState(string taskState)
        {
            return _dbSet.Where(t => t.State.name == taskState).ToList();
        }

        //Search a unique task by id and userId
        public Task GetUserTaskById(int taskId, string userId)
        {
            return _context.Tasks.FirstOrDefault(t => t.id == taskId && t.id_user == userId);
        }

        public List<Task> FindTaskByUser(string id_user)
        {
            return _dbSet.Where(t => t.id_user == id_user)
                         .Include(t => t.Priority)
                         .Include(t => t.State)
                         .ToList();
        }

        public List<Task> OrderByPriorities(string userId, int? id_state, int? id_priority)
        {
            var query = _dbSet.Include(t => t.Priority)
                      .Include(t => t.State)
                      .Where(t => t.id_user == userId);

            if (id_state.HasValue)
                query = query.Where(t => t.id_state == id_state.Value);

            if (id_priority.HasValue)
                query = query.Where(t => t.id_priority == id_priority.Value);

            return query.ToList();

        }

    }
}