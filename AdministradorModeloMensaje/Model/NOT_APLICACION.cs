using System;
using System.Collections.Generic;
using System.Text;

namespace SondaMQ.AdministradorModeloMensaje.Model
{
    public class NOT_APLICACION
    {
        public virtual Guid id_not_aplicacion { get; set; }
        public virtual string codigo { get; set; }
        public virtual string descripcion { get; set; }
        public virtual int obligatorio { get; set; }
    }
}
