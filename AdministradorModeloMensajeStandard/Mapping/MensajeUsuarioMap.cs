using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using SondaMQ.AdministradorModeloMensaje.Model;

namespace SondaMQ.AdministradorModeloMensaje.Mapping
{
    class MensajeUsuarioMap : ClassMap<NOT_MENSAJE_USUARIO>
    {
        MensajeUsuarioMap()
        {
            Table("NOT_MENSAJE_USUARIO");
            Not.LazyLoad();
            Id(x => x.id_not_mensaje_usuario).Column("ID_NOT_MENSAJE_USUARIO").GeneratedBy.Guid();

            Map(x => x.id_not_mensaje).Column("ID_NOT_MENSAJE");
            Map(x => x.id_usuario).Column("ID_USUARIO");
            Map(x => x.idioma).Column("IDIOMA");
            Map(x => x.estado).Column("ESTADO");
            Map(x => x.fecha_entrega).Column("FECHA_ENTREGA");
        }
    }
}
