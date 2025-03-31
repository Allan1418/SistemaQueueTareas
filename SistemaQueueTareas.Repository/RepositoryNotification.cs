using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaQueueTareas.Data;

namespace SistemaQueueTareas.Repository
{
    public interface IRepositoryNotification : IRepositoryBase<Notification>
    {
    }
    public class RepositoryNotification : RepositoryBase<Notification>, IRepositoryNotification
    {
        public RepositoryNotification() : base()
        {
        }

        public List<Notification> findAllNotification()
        {
            return _dbSet.ToList();
        }

        public List<Notification> findNotificationByUser(string idUser)
        {
            return _dbSet.Where(n => n.id_user == idUser).ToList();
        }
    }
}
