﻿using System;
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
        private RepositoryState _repositoryState;

        private readonly State _enPausaState;
        private readonly State _PendienteState;
        private readonly State _FallidoState;

        public TaskManager()
        {
            _repositoryTask = new RepositoryTask();
            _repositoryState = new RepositoryState();
            _enPausaState = _repositoryState.GetStateByName("En Pausa");
            _PendienteState = _repositoryState.GetStateByName("Pendiente");
            _FallidoState = _repositoryState.GetStateByName("Fallida");

        }

        public Task getEnProcesoTask()
        {
            var retorno = _repositoryTask.findTaskByState("En Proceso");
            if (retorno.Count == 0)
            {
                return null;
            }
            else
            {
                return retorno.First();
            }
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
            task.id_state = _enPausaState.id;
            _repositoryTask.Add(task);
        }

        public void UpdateTask(Task task)
        {
            // Verificar si task.State no es null antes de acceder a task.State.name
            // Tambien verifica que no sea ninguno de esos otros estados
            if (task.State != null &&
                (task.State.name == "Pendiente" ||
                 task.State.name == "En Cola" ||
                 task.State.name == "En Proceso"))
            {
                return;
            }

            // Si llegamos aquí, significa que task.State es no-null y el nombre no coincide
            string log = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] La tarea se editó.\n";
            task.log += log;

            // Llamar al repositorio para actualizar la tarea
            _repositoryTask.Update(task);
        }


        public void DeleteTask(Task task)
        {
            if (task.State.name == "Pendiente" ||
                task.State.name == "En Cola" ||
                task.State.name == "En Proceso"
                )
            {
                return;
            }
            _repositoryTask.Delete(task.id);
        }

        public void FindByState(string taskState)
        {

            _repositoryTask.findTaskByState(taskState);

        }

        public List<Task> OrderByProritiesTask(string userId, int? id_state, int? id_priority)
        {
            return _repositoryTask.OrderByPriorities(userId, id_state, id_priority);
        }


        //Necesario para obtener las tareas de un usuario utilizando el id del usuario
        public Task GetUserTaskById(int taskId, string userId)
        {
            return _repositoryTask.GetUserTaskById(taskId, userId);
        }


        
        public void agregarACola(Task task)
        {
            if (task.State.name != "En Pausa")
            {
                return;
            }
            task.id_state = _PendienteState.id;
            _repositoryTask.Update(task);
        }


        public void agreagarAColaBatch(List<Task> tasks)
        {
            foreach (var task in tasks)
            {
                agregarACola(task);
            }
        }


        public void Reintentar(Task task)
        {
            if (task.State.name != "Fallida")
            {
                return;
            }
            task.id_state = _PendienteState.id;
            _repositoryTask.Update(task);
        }
    }
}
