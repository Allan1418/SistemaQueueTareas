using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaQueueTareas.Data
{
    public interface IEntity
    {
        string UniqueId { get; set; }
    }

    public class Entity : IEntity
    {
        public string UniqueId { get; set; }
    }
}
