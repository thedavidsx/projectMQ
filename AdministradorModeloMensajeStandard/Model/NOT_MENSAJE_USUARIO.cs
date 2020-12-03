using System;
using System.Collections.Generic;
using System.Text;

namespace SondaMQ.AdministradorModeloMensaje.Model
{
    public class NOT_MENSAJE_USUARIO
    {
        public virtual Guid id_not_mensaje_usuario { get; set; }
        public virtual Guid id_not_mensaje { get; set; }
        public virtual string id_usuario { get; set; }
        public virtual string idioma { get; set; }
        public virtual int estado { get; set; }
        public virtual DateTime fecha_entrega { get; set; }
    }
}
