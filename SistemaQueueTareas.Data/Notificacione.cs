//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaQueueTareas.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notificacione
    {
        public int id_notificacion { get; set; }
        public int id_usuario { get; set; }
        public string mensaje { get; set; }
        public Nullable<System.DateTime> fecha_envio { get; set; }
        public Nullable<bool> leido { get; set; }
    
        public virtual User User { get; set; }
    }
}
