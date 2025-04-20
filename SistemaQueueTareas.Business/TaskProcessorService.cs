using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TaskEntity = SistemaQueueTareas.Data.Task;
using StateEntity = SistemaQueueTareas.Data.State;
using NotificationEntity = SistemaQueueTareas.Data.Notification;
using SistemaQueueTareas.Repository;

namespace SistemaQueueTareas.Business
{
    public class TaskProcessorService : IHostedService
    {
        private readonly PriorityTaskQueue _taskQueue;
        private readonly RepositoryTask _taskRepository;
        private readonly RepositoryState _stateRepository;
        private readonly RepositoryNotification _notificationRepository;

        private readonly StateEntity _enProcesoState;
        private readonly StateEntity _finalizadaState;
        private readonly StateEntity _fallidaState;
        private readonly StateEntity _enColaState;

        private Timer _timer;

        private readonly object _lock = new object();

        private static readonly Random _random = new Random();

        public TaskProcessorService(
            PriorityTaskQueue taskQueue,
            RepositoryTask taskRepository,
            RepositoryState stateRepository,
            RepositoryNotification notificationRepository
            )
        {
            _taskQueue = taskQueue;
            _taskRepository = taskRepository;
            _stateRepository = stateRepository;
            _notificationRepository = notificationRepository;

            _enProcesoState = stateRepository.GetStateByName("En Proceso");
            _finalizadaState = stateRepository.GetStateByName("Finalizada");
            _fallidaState = stateRepository.GetStateByName("Fallida");
            _enColaState = stateRepository.GetStateByName("En Cola");
            
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            var enProcesoTasks = _taskRepository.FindTaskByState(_enProcesoState.id);
            foreach (var task in enProcesoTasks)
            {
                task.id_state = _fallidaState.id;
                task.log += "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] Tarea Fallida por error del servidor.\n";
                _taskRepository.Update(task);
                SendNotification(task.id_user, $"Fallo la ejecucion de la Tarea ({task.name}). Por favor intenta de nuevo.");
            }

            var enColaTasks = _taskRepository.FindTaskByState(_enColaState.id);
            foreach(var task in enColaTasks)
            {
                _taskQueue.Enqueue(task);
            }


            _timer = new Timer(ProcessTasks, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void ProcessTasks(object state)
        {
            // Un lock para evitar que multiples hilos ejecuten una tarea al mismo tiempo
            bool lockTaken = false;


            // Obtener tareas pendientes desde el repositorio
            var pendingTasks = _taskRepository.GetPendingTasks();
            foreach (var task in pendingTasks)
            {
                string log = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] Se agrego la Tarea a la cola Prioridad(" + task.Priority.name + ")\n";
                task.log += log;
                task.id_state = _enColaState.id;
                _taskRepository.Update(task);
                SendNotification(task.id_user, $"La Tarea ({task.name}) se agrego A la cola.");

                _taskQueue.Enqueue(task);
            }

            try
            {
                Monitor.TryEnter(_lock, 0, ref lockTaken);

                if (lockTaken)
                {
                    var nextTask = _taskQueue.Dequeue();
                    if (nextTask != null)
                    {
                        ExecuteTask(nextTask);
                    }
                }
                else
                {
                    // Lock NO adquirido (otra ejecucion esta en curso).
                    // Simple return
                    return;
                }
            }
            finally
            {
                // Asegurarse de liberar el lock SIEMPRE si lo adquirimos
                if (lockTaken)
                {
                    Monitor.Exit(_lock);
                }
            }

        }

        private void ExecuteTask(TaskEntity task)
        {
            try
            {
                // Actualizar estado a "En Proceso"
                task.id_state = _enProcesoState.id;
                task.fecha_ejecucion = DateTime.Now;
                _taskRepository.Update(task);
                SendNotification(task.id_user, $"Empezo la ejecucion de la Tarea {task.name}.");

                // Simular ejecucion - 10 segundos
                Thread.Sleep(10000);


                // Simular fallo con 40% de probabilidad
                var chance = _random.NextDouble();
                if (chance < 0.40)
                {
                    throw new InvalidOperationException($"Fallo simulado en la tarea {task.id}");
                }

                // Actualizar estado a "Finalizada"
                task.id_state = _finalizadaState.id;
                task.fecha_finalizacion = DateTime.Now;
                task.log += "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] Tarea Finalizada\n";
                _taskRepository.Update(task);
                SendNotification(task.id_user, $"Finalizo la ejecucion de la Tarea ({task.name}) satisfactoriamente.");
            }
            catch (Exception ex)
            {
                // Actualizar estado a "Fallida"
                task.id_state = _fallidaState.id;
                task.log += "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] Tarea Fallida: " + ex.Message + "\n";
                _taskRepository.Update(task);
                SendNotification(task.id_user, $"Fallo la ejecucion de la Tarea ({task.name}). Por favor intenta de nuevo.");
            }
        }

        private void SendNotification(string idUser, string txt)
        {
            NotificationEntity notification = new NotificationEntity();
            notification.id_user = idUser;
            notification.mensaje = txt;
            _notificationRepository.Add(notification);

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
