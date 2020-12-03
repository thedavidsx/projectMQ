using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SondaMQ.ProveedorNotificacionBus.Modelo
{
    public class MensajeUsuarioModel
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string canal { get; set; }
    }
}
