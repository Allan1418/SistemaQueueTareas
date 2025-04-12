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
    public class RepositoryTask : RepositoryBase<Task>, IRepositoryTask
    {
        public RepositoryTask() : base()
        {
        }

        public IEnumerable<Task> GetPendingTasks()
        {
            return _context.Tasks
                .Include(t => t.Priority)
                .Where(t => t.State.name == "Pendiente")
                .ToList();
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

        //Estado inicial de un task cuando se crea
        public int GetStateIdByName(string stateName)
        {
            return _context.States.FirstOrDefault(s => s.name == stateName)?.id ?? 0;
        }

        //This method is used to get the state of a task
        public List<State> GetAllStates()
        {
            return _context.States.ToList();
        }



        //show a detail of a task
        public Task GetDetailTask(int id, string userId)
        {
            return _dbSet.Include(t => t.Priority)// the include is useful because it allows to get the name of the priority
                         .Include(t => t.State)     // those kind of includes are named "eager loading"
                         .FirstOrDefault(t => t.id == id && t.id_user == userId);
        }


        //Search a unique task by id and userId
        public Task GetUserTaskById(int taskId, string userId)
        {
            return _context.Tasks.FirstOrDefault(t => t.id == taskId && t.id_user == userId);
        }

        //create to validate a task
        public bool TaskExists(int taskId)
        {
            return _context.Tasks.Any(t => t.id == taskId);
        }


        public void ObjectModified(Task task)
        {
            _context.Entry(task).State = EntityState.Modified;
            Save();
        }

    }
}