using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Web.Controllers
{
    public class TasksController : Controller
    {
        private SistemaQueueTareasContext db = new SistemaQueueTareasContext();

        [Authorize]
        public ActionResult Index(int? id_state, int? id_priority)
        {
            var userId = User.Identity.GetUserId();
            IQueryable<Task> tasks = db.Tasks
                .Include(t => t.Priority)
                .Include(t => t.State)
                .Where(t => t.id_user == userId);

            if (id_state.HasValue)
                tasks = tasks.Where(t => t.id_state == id_state.Value);

            if (id_priority.HasValue)
                tasks = tasks.Where(t => t.id_priority == id_priority.Value);

            ViewBag.States = new SelectList(db.States, "id", "name", id_state);
            ViewBag.PrioritiesFilter = new SelectList(db.Priorities.OrderBy(p => p.order), "id", "name", id_priority);

            return View(tasks.ToList());
        }

        [Authorize]
        public ActionResult GetTask(int id)
        {
            var userId = User.Identity.GetUserId();
            var task = db.Tasks
                .Include(t => t.Priority)
                .Include(t => t.State)
                .FirstOrDefault(t => t.id == id && t.id_user == userId);

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
                    priorities = db.Priorities.OrderBy(p => p.order).Select(p => new { id = p.id, name = p.name }).ToList(),
                    states = db.States.Select(s => new { id = s.id, name = s.name }).ToList()
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
                var existingTask = db.Tasks.FirstOrDefault(t => t.id == task.id && t.id_user == userId);

                if (existingTask == null)
                    return Json(new { success = false, errors = "Acceso denegado o tarea no existe" });

                existingTask.name = task.name;
                existingTask.description = task.description;
                existingTask.id_priority = task.id_priority;

                if (!db.Priorities.Any(p => p.id == task.id_priority))
                    return Json(new { success = false, errors = "Prioridad no válida" });

                if (ModelState.IsValid)
                {
                    db.Entry(existingTask).State = EntityState.Modified;
                    db.SaveChanges();
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
            Task task = db.Tasks.FirstOrDefault(t => t.id == id && t.id_user == userId);

            if (task == null) return HttpNotFound();
            return View(task);
        }

        public ActionResult Create()
        {
            ViewBag.id_priority = new SelectList(db.Priorities, "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "name,description,id_priority")] Task task)
        {
            if (ModelState.IsValid)
            {
                task.id_user = User.Identity.GetUserId();
                task.id_state = db.States.FirstOrDefault(s => s.name == "Pendiente").id;
                task.fecha_creacion = DateTime.Now;

                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_priority = new SelectList(db.Priorities, "id", "name", task.id_priority);
            return View(task);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userId = User.Identity.GetUserId();
            Task task = db.Tasks.FirstOrDefault(t => t.id == id && t.id_user == userId);

            if (task == null) return HttpNotFound();

            if (task.State.name == "En Proceso")
            {
                TempData["ErrorMessage"] = "No se puede editar tareas en proceso";
                return RedirectToAction("Index");
            }

            ViewBag.id_priority = new SelectList(db.Priorities, "id", "name", task.id_priority);
            ViewBag.id_state = new SelectList(db.States, "id", "name", task.id_state);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_user,name,description,id_priority,id_state,fecha_creacion,fecha_ejecucion,fecha_finalizacion")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_priority = new SelectList(db.Priorities, "id", "name", task.id_priority);
            ViewBag.id_state = new SelectList(db.States, "id", "name", task.id_state);
            return View(task);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var userId = User.Identity.GetUserId();
            Task task = db.Tasks.FirstOrDefault(t => t.id == id && t.id_user == userId);

            if (task == null) return HttpNotFound();

            if (task.State.name == "En Proceso")
            {
                TempData["ErrorMessage"] = "No se puede eliminar tareas en proceso";
                return RedirectToAction("Index");
            }

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Execute(int id)
        {
            var task = db.Tasks.Find(id);
            if (task == null) return HttpNotFound();

            if (task.id_user != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            var estadoEnProceso = db.States.FirstOrDefault(s => s.name == "En Proceso");
            if (estadoEnProceso == null)
            {
                TempData["ErrorMessage"] = "Estado no configurado";
                return RedirectToAction("Index");
            }
            if (estadoEnProceso != null)
            {
                task.id_state = estadoEnProceso.id;
                task.fecha_ejecucion = DateTime.Now;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}