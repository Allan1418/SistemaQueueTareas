using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;

using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SistemaQueueTareas.Business;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Web.Controllers
{

    public class TasksController : Controller
    {
        //private SistemaQueueTareasContext db = new SistemaQueueTareasContext();
        private TaskManager _taskManager = new TaskManager();
        private PriorityManager _priorityManager = new PriorityManager();
        private StateManager _stateManager = new StateManager();

        // GET: Tasks
        [Authorize]
        public ActionResult Index(int? id_state, int? id_priority)
        {
            var userId = User.Identity.GetUserId();
            var tasks = _taskManager.OrderByProritiesTask(userId, id_state, id_priority);
            /*IQueryable<Task> tasks = db.Tasks
                .Include(t => t.Priority)
                .Include(t => t.State)
                .Where(t => t.id_user == userId);*/

            ViewBag.States = new SelectList(_taskManager.GetAllStates(), "id", "name", id_state);
            ViewBag.PrioritiesFilter = new SelectList(_priorityManager.GetAllOrderPriorities(), "id", "name", id_priority);

            return View(tasks.ToList());
        }

        [Authorize]
        public ActionResult GetTask(int id)
        {
            var userId = User.Identity.GetUserId();
            var task = _taskManager.GetDetailTask(id, userId);

            if (task == null)
                return Json(new { success = false, message = "Tarea no encontrada." }, JsonRequestBehavior.AllowGet);

            return Json(new
            {
                success = true,
                task = new
                {
                    task.id,
                    task.name,
                    task.description,
                    task.id_priority,
                    task.id_state,
                    priorityName = task.Priority?.name,
                    stateName = task.State?.name,
                    priorities = _priorityManager.GetAllOrderPriorities().Select(p => new { id = p.id, name = p.name }).ToList(),
                    states = _taskManager.GetAllStates().Select(s => new { id = s.id, name = s.name }).ToList()
                }
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTaskModal(Task task)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var existingTask = _taskManager.GetUserTaskById(task.id, userId);

                if (existingTask == null)
                    return Json(new { success = false, errors = "Acceso denegado o tarea no existe" });

                existingTask.name = task.name;
                existingTask.description = task.description;
                existingTask.id_priority = task.id_priority;

                if (!_priorityManager.PriorityExists(task.id_priority))
                    return Json(new { success = false, errors = "Prioridad no válida" });


                if (ModelState.IsValid)
                {
                    _taskManager.TaskModified(task);
                    _taskManager.SaveTask();
                    return Json(new { success = true });
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new { success = false, errors = "Error interno del servidor." });
            }
        }





        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userId = User.Identity.GetUserId();
            Task task = _taskManager.GetUserTaskById(id.Value, userId);

            if (task == null) return HttpNotFound();
            return View(task);
        }

        public ActionResult Create()
        {
            ViewBag.id_priority = new SelectList(_priorityManager.GetAllOrderPriorities(), "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "name,description,id_priority")] Task task)
        {
            /*if (ModelState.IsValid)
            {
                task.id_user = User.Identity.GetUserId();
                task.id_state = db.States.FirstOrDefault(s => s.name == "Pendiente").id;
                task.fecha_creacion = DateTime.Now;

                _taskManager.AddTask(task);
                _taskManager.SaveTask();
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/
            if (ModelState.IsValid)
            {
                task.id_user = User.Identity.GetUserId();
                _taskManager.InitialStateTask(task);
                task.fecha_creacion = DateTime.Now;

                _taskManager.AddTask(task);
                _taskManager.SaveTask();
                return RedirectToAction("Index");
            }
            ViewBag.id_priority = new SelectList(_priorityManager.GetAllOrderPriorities(), "id", "name", task.id_priority);

            return View(task);
        }


        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userId = User.Identity.GetUserId();
            Task task = _taskManager.GetUserTaskById(id.Value, userId);

            if (task == null) return HttpNotFound();

            if (task.State.name == "En Proceso")
            {
                TempData["ErrorMessage"] = "No se puede editar tareas en proceso";
                return RedirectToAction("Index");
            }

            ViewBag.id_priority = new SelectList(_priorityManager.GetAllOrderPriorities(), "id", "name", task.id_priority);
            ViewBag.States = new SelectList(_taskManager.GetAllStates(), "id", "name", task.id_state);

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_user,name,description,id_priority,id_state,fecha_creacion,fecha_ejecucion,fecha_finalizacion")] Task task)
        {
            if (ModelState.IsValid)
            {
                _taskManager.TaskModified(task);
                _taskManager.SaveTask();
                return RedirectToAction("Index");
            }
            ViewBag.id_priority = new SelectList(_priorityManager.GetAllOrderPriorities(), "id", "name", task.id_priority);
            ViewBag.id_state = new SelectList(_taskManager.GetAllStates(), "id", "name", task.id_state);
            return View(task);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userId = User.Identity.GetUserId();
            Task task = _taskManager.GetUserTaskById(id.Value, userId);

            if (task == null) return HttpNotFound();

            if (task.State.name == "En Proceso")
            {
                TempData["ErrorMessage"] = "No se puede eliminar tareas en proceso";
                return RedirectToAction("Index");
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = _taskManager.GetTaskById(id);
            
            _taskManager.DeleteTask(id);
            _taskManager.SaveTask();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Execute(int id)
        {
            var task = _taskManager.GetTaskById(id);
            if (task == null) return HttpNotFound();

            if (task.id_user != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            var estadoEnProceso = _stateManager.StateInProcess("En Proceso");
            if (estadoEnProceso == null)
            {
                TempData["ErrorMessage"] = "Estado no configurado";
                return RedirectToAction("Index");
            }
            if (estadoEnProceso != null)
            {
                task.id_state = estadoEnProceso.id;
                task.fecha_ejecucion = DateTime.Now;
                _taskManager.SaveTask();
            }

            return RedirectToAction("Index");
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
