using System;
using System.Collections.Generic;
using System.Text;

namespace SondaMQ.AdministradorModeloMensaje.Model
{
    public class NOT_CATEGORIA
    {
        public virtual Guid id_not_categoria { get; set; }
        public virtual string codigo_not_aplicacion { get; set; }
        public virtual string codigo { get; set; }
        public virtual string descripcion { get; set; }
    }
}
