using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SistemaQueueTareas.Data;
using SistemaQueueTareas.Repository;
using SistemaQueueTareas.Business;

namespace SistemaQueueTareas.Web.Controllers
{
    public class HomeController : Controller
    {
        private TaskManager _taskManager = new TaskManager();
        RepositoryNotification _repositoryNotification = new RepositoryNotification();

        public ActionResult Index()
        {
            var tasks = _taskManager.GetAllTasks();
            return View(tasks);
        }

        public ActionResult FindTaskByState(string taskState)
        {
            if (string.IsNullOrEmpty(taskState))
            {
                ViewBag.Message = "Por favor, ingrese un estado.";
                return View("Index", new List<Task>());
            }

            _taskManager.FindByState(taskState);
            return View("Index");
        }

        // Métodos para notificaciones (modal)
        public ActionResult FindAllNotification()
        {
            var notifications = _repositoryNotification.findAllNotification();
            return PartialView("_NotificationsPartial", notifications);
        }

        public ActionResult FindUserNotifications(string idUser)
        {
            var notifications = string.IsNullOrEmpty(idUser)
                ? _repositoryNotification.findAllNotification()
                : _repositoryNotification.findNotificationByUser(idUser);

            return PartialView("_NotificationsPartial", notifications);
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