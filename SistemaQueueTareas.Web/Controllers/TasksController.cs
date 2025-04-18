﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
            var taskEnProceso = _taskManager.getEnProcesoTask();

            ViewBag.PrioritiesFilter = new SelectList(_priorityManager.GetAllOrderPriorities(), "id", "name", id_priority);


            return View(tasks.ToList());
        }


        //Este metodo obtiene todos los task de un usuario para insertarlos en la tabla
        [Authorize]
        public ActionResult GetTask(int id)
        {
            var userId = User.Identity.GetUserId();//obtener el id del usuario logueados

            var task = _taskManager.GetUserTaskById(id, userId);//obtener la tarea por id y el id del usuario logueado

            if (task == null)
                return HttpNotFound("La tarea no ha sido encontrada");

            return PartialView("Index", task);


        }

        //renderiza el formulario de edicion de tareas en blanco
        [HttpGet]
        public ActionResult EditTaskModal(int id)
        {
            var task = _taskManager.GetTaskById(id);
            
            ViewBag.id_priority = new SelectList(_priorityManager.GetAllOrderPriorities(), "id", "name", task.id_priority);
            return View("Edit",task);
        }

        //Metodo de edicion de tareas
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTaskModal(Task task)
        {

            if (ModelState.IsValid)
            {

                _taskManager.UpdateTask(task);
                return RedirectToAction("Index");
            }

            ViewBag.id_priority = _priorityManager.GetAllOrderPriorities();
            return View("Edit",task);

        }



        //Este metodo muestra una renderizacion del formulario en blanco para crear una nueva tarea
        //ademas de llenar el dropdown con las prioridades
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.id_priority = new SelectList(_priorityManager.GetAllOrderPriorities(), "id", "name");
            return View();
        }
        //Este metodo realiza un post de los datos llenados en el formulario
        //para crear una nueva tarea
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                task.id_user = User.Identity.GetUserId();
                if (task.id_user == null)
                {
                    return HttpNotFound("El usuario no ha sido encontrado");
                }
                task.fecha_creacion = DateTime.Now;
                _taskManager.AddTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }


        //Este metodo realiza la verificacion de la tarea a eliminar
        [Authorize]
        public ActionResult Delete(int? id)
        {
            var userId = User.Identity.GetUserId();

            Task task = _taskManager.GetUserTaskById(id.Value, userId);

            //validacion de tarea
            if (task == null) return HttpNotFound("No se encontro la tarea");

            //Se valida si la tarea aun no ha sido procesada
            if (task.State.name == "En Proceso")
            {
                TempData["ErrorMessage"] = "No se puede eliminar tareas en proceso";
                return RedirectToAction("Index");
            }

            return View(task);
        }

        //Este metodo realiza la eliminacion de la tarea 
        //Cuando se selecciona el boton de confirmar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = _taskManager.GetTaskById(id);

            _taskManager.DeleteTask(task);

            return RedirectToAction("Index");
        }


        [HttpPost]
        [Authorize]
        public ActionResult ExecuteTask(int id)
        {
            var task = _taskManager.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound("La tarea no ha sido encontrada");
            }

            _taskManager.agregarACola(task);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult ExecuterTaskBatch(List<int> ids)
        {
            if (ids == null)
            {
                TempData["ErrorMessage"] = "No se seleccionaron tareas para ejecutar.";
                return RedirectToAction("Index");
            }

            var tasks = ids.Select(id => _taskManager.GetTaskById(id)).Where(t=> t!= null).ToList();
            _taskManager.agreagarAColaBatch(tasks);
            return RedirectToAction("Index");

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

        [HttpPost]
        [Authorize]
        public ActionResult Retry(int id)
        {
            var task = _taskManager.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound("La tarea no ha sido encontrada");
            }
            _taskManager.Reintentar(task);
            return RedirectToAction("Index");
        }

    }
}