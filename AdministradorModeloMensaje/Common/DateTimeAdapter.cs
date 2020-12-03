using System;

namespace SondaMQ.AdministradorModeloMensaje.Common
{
    public class DateTimeAdapter : IDateTime
    {
        public DateTime UtcNow
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
