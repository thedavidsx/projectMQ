using System;
using System.Collections.Generic;
using System.Text;

namespace SondaMQ.AdministradorModeloMensaje.Model
{
    public class NOT_OPCIONES_USUARIO
    {
        public virtual Guid id_not_opciones_usuario { get; set; }
        public virtual string id_usuario { get; set; }
        public virtual long configuracion { get; set; }
    }
}
