using System;

namespace SondaMQ.AdministradorBusMensajeStandard
{
    public interface ContratoSuscriptor
    {
        string Mensaje { get; set; }
        string Usuario { get; set; }
        bool IsOk { get; set; }
        string Asunto { get; set; }
        string Filtro { get; set; }
        Guid IdmensajeUsuario { get; set; }
    }
}