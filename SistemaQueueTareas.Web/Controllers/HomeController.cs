using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SistemaQueueTareas.Data;
using SistemaQueueTareas.Repository;
using SistemaQueueTareas.Business;
using Microsoft.AspNet.Identity;


namespace SistemaQueueTareas.Web.Controllers
{
    public class HomeController : Controller
    {
        
        private NotificationManager _notificationManager = new NotificationManager();


        public ActionResult Index()
        { 
 
            return View("Index");
            
        }

        [Authorize]
        public ActionResult FindUserNotifications()
        {
            string idUser = User.Identity.GetUserId();
            
            var notifications = _notificationManager.getNotificationsByUser(idUser);

            if (notifications == null || !notifications.Any())
            {
                ViewBag.Message = "No se encontraron notificaciones para este usuario.";
                return View("Notifications", new List<Notification>());
            }

            return PartialView("_NotificationsPartial", notifications);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ReadNotification(int idNotification)
        { 
            _notificationManager.readNotification(idNotification);
            return RedirectToAction("FindUserNotifications");
        }

    }
}