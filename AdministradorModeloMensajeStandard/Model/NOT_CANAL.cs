using System;
using System.Collections.Generic;
using System.Text;

namespace SondaMQ.AdministradorModeloMensaje.Model
{
    public class NOT_CANAL
    {
        public virtual string codigo_not_canal { get; set; }
        public virtual string url { get; set; }
        public virtual decimal max_reintentos { get; set; }
        public virtual decimal tiempo_reintentos { get; set; }
        public virtual decimal canal_obligatorio { get; set; }
        public virtual string formato { get; set; }
    }
}
