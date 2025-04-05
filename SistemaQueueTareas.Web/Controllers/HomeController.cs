using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaQueueTareas.Data;
using SistemaQueueTareas.Repository;

namespace SistemaQueueTareas.Web.Controllers
{
    public class HomeController : Controller
    {
        private RepositoryTask _repositoryTask = new RepositoryTask();
        RepositoryNotification _repositoryNotification = new RepositoryNotification();


        //metodos para la tabla de task
        public ActionResult Index()
        { 
            var tasks = _repositoryTask.findAllTask(); // Obtener tareas desde la base de datos
            return View(tasks);
            
        }

        public ActionResult FindUserTask(string idUser)
        {
            if (string.IsNullOrEmpty(idUser))
            {
                ViewBag.Message = "Por favor, ingrese un ID de usuario.";
                return View("Index", new List<Task>()); // Devuelve una lista vacía
            }

            var taskUser = _repositoryTask.findTaskByUser(idUser);

            if (taskUser == null || !taskUser.Any())
            {
                ViewBag.Message = "No se encontraron tareas para este usuario.";
                return View("Index", new List<Task>());
            }

            return View("Index", taskUser);
            
        }

        public ActionResult FindTaskByState(string taskState)
        {

            //genera una alerta en caso de que no se ingrese ningun dato en el input
            if (string.IsNullOrEmpty(taskState))
            {
                ViewBag.Message = "Por favor, ingrese un estado.";
                return View("Index", new List<Task>());
            }

            var tasks = _repositoryTask.findTaskByState(taskState);

            //En el input se ingreso un dato incorrecto para la busqueda
            if (tasks == null || !tasks.Any())
            {
                ViewBag.Message = "No se encontraron tareas con este estado.";
                return View("Index", new List<Task>());
            }

            return View("Index", tasks);
        }


        //metodos para la tabla de notifications
        public ActionResult FindAllNotification()
        {
            var notifications = _repositoryNotification.findAllNotification();
            return View("Contact", notifications);
        }

        public ActionResult FindUserNotifications(string idUser)
        {
            if (string.IsNullOrEmpty(idUser))
            {
                ViewBag.Message = "Por favor, ingrese un ID de usuario.";
                return View("Notifications", new List<Notification>());
            }

            var notifications = _repositoryNotification.findNotificationByUser(idUser);

            if (notifications == null || !notifications.Any())
            {
                ViewBag.Message = "No se encontraron notificaciones para este usuario.";
                return View("Notifications", new List<Notification>());
            }

            return View("Contact", notifications);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}