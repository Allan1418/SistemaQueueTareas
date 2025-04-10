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


    }
}
