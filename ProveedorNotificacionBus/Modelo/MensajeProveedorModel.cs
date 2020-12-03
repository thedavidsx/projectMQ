using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SondaMQ.ProveedorNotificacionBus.Modelo
{
    public class MensajeProveedorModel
    {
        public string mensaje { get; set; }
        public string asunto { get; set; }
        public string desitinatarios { get; set; }
        //public string canal { get; set; }
        public string formato { get; set; }
        public int prioridad { get; set; }
        public string cod_aplicacion { get; set; }
        public List<MensajeUsuarioModel> listausuarios { get; set; }
    }
}
