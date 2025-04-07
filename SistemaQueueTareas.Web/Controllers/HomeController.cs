using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SistemaQueueTareas.Data;
using SistemaQueueTareas.Repository;

namespace SistemaQueueTareas.Web.Controllers
{
    public class HomeController : Controller
    {
        private RepositoryNotification _repositoryNotification = new RepositoryNotification();

        public ActionResult Index()
        {
            return View();
        }

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