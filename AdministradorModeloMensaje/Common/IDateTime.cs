using System;
using System.Collections.Generic;
using System.Text;

namespace SondaMQ.AdministradorModeloMensaje.Common
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}
