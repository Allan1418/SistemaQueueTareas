using SistemaQueueTareas.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Business
{
    public class NotificationManager
    {
        RepositoryNotification _repositoryNotification;
        public NotificationManager()
        {
            _repositoryNotification = new RepositoryNotification();
        }

        public List<Notification> getNotificationsByUser(string idUser)
        {
            return _repositoryNotification.findNotificationByUser(idUser);
        }

        public void readNotification(int idNotification)
        {
            Notification notification = _repositoryNotification.GetById(idNotification);
            if (notification != null)
            {
                notification.leido = true;
                _repositoryNotification.Update(notification);
            }
        }

    }
}
