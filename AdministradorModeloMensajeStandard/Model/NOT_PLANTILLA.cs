using System;
using System.Collections.Generic;
using System.Text;

namespace SondaMQ.AdministradorModeloMensaje.Model
{
    public class NOT_PLANTILLA
    {
        public virtual Guid id_not_plantilla { get; set; }
        public virtual string id_not_canal { get; set; }
        public virtual string formato { get; set; }
        public virtual long contenido { get; set; }
    }
}
