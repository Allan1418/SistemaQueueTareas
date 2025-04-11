using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SistemaQueueTareas.Data;
using SistemaQueueTareas.Repository;

namespace SistemaQueueTareas.Business
{
    public class TaskManager
    {
        private RepositoryTask _repositoryTask;

        public TaskManager()
        {
            _repositoryTask = new RepositoryTask();
        }

        public IEnumerable<Task> GetAllTasks()
        {
            var tasks = _repositoryTask.GetAll().ToList();
            return tasks;
        }
        public Task GetTaskById(int id)
        {
            var task = _repositoryTask.GetById(id);
            return task;
        }

        public void AddTask(Task task)
        {
            _repositoryTask.Add(task);
        }

        public void UpdateTask(Task task)
        {
            _repositoryTask.Update(task);
        }

        public void DeleteTask(int id)
        {
            _repositoryTask.Delete(id);
        }
        public void SaveTask()
        {
            _repositoryTask.Save();
        }

        public void FindByState(string taskState)
        {

            _repositoryTask.findTaskByState(taskState);

        }

        public List<Task> OrderByProritiesTask(string userId, int? id_state, int? id_priority)
        {
            return _repositoryTask.OrderByPriorities(userId, id_state, id_priority);
        }


        //este metodo agrega el metodo definido a la hora de crear un task
        public void InitialStateTask(Task task)
        {
            task.id_state = _repositoryTask.GetStateIdByName("Pendiente");
            
        }

        public List<State> GetAllStates()
        {
            return _repositoryTask.GetAllStates();
        }

        public Task GetDetailTask(int id, string userId)
        {
            return _repositoryTask.GetDetailTask(id, userId);
        }

        public Task GetUserTaskById(int taskId, string userId)
        {
            return _repositoryTask.GetUserTaskById(taskId, userId);
        }

        public bool TaskExists(int taskId)
        {
            return _repositoryTask.TaskExists(taskId);
        }

        public void TaskModified(Task task)
        {
            _repositoryTask.ObjectModified(task);
        }
    }
}
