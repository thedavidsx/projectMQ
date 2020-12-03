using System;
using System.Collections.Generic;
using System.Text;

namespace SondaMQ.AdministradorModeloMensaje.Model
{
    public class NOT_MENSAJE
    {
        public virtual Guid id_not_mensaje { get; set; }
        public virtual string mensaje { get; set; }
        public virtual string formato { get; set; }
        public virtual Guid id_not_mensaje_usuario { get; set; }
        public virtual int prioridad { get; set; }
        public virtual string nivel { get; set; }
        public virtual DateTime fecha_envio { get; set; }
        public virtual string destinatarios { get; set; }
        public virtual string icono { get; set; }
        public virtual string canal_destinatario { get; set; }
        public virtual Guid id_categoria { get; set; }
        public virtual Guid id_aplicacion { get; set; }
    }
}
