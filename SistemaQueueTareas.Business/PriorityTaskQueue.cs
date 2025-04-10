using System;
using System.Collections.Generic;
using System.Linq;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Business
{
    public class PriorityTaskQueue
    {
        private readonly SortedDictionary<int, Queue<Task>> _queues;
        private readonly object _lock = new object();

        public PriorityTaskQueue(IEnumerable<Priority> priorities)
        {
            // Ordenar prioridades de mayor a menor (Alta=3, Media=2, Baja=1)
            var orderedPriorities = priorities
                .OrderByDescending(p => p.order)
                .ToDictionary(p => p.id, p => p.order);

            _queues = new SortedDictionary<int, Queue<Task>>(
                Comparer<int>.Create((x, y) => y.CompareTo(x)) // Orden descendente
            );

            // Inicializar colas por cada nivel de prioridad
            foreach (var priority in orderedPriorities.Values.Distinct())
            {
                _queues[priority] = new Queue<Task>();
            }
        }

        public void Enqueue(Task task)
        {
            lock (_lock)
            {
                var priorityOrder = task.Priority.order;
                if (!_queues.ContainsKey(priorityOrder))
                {
                    throw new InvalidOperationException($"Prioridad {priorityOrder} no configurada.");
                }

                _queues[priorityOrder].Enqueue(task);
            }
        }

        public Task Dequeue()
        {
            lock (_lock)
            {
                foreach (var queue in _queues.Values)
                {
                    if (queue.Count > 0)
                    {
                        return queue.Dequeue();
                    }
                }
                return null;
            }
        }
    }
}
