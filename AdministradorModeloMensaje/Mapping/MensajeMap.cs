using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using SondaMQ.AdministradorModeloMensaje.Model;

namespace SondaMQ.AdministradorModeloMensaje.Mapping
{
    class MensajeMap : ClassMap<NOT_MENSAJE>
    {
        MensajeMap()
        {
            Table("NOT_MENSAJE");
            Not.LazyLoad();
            Id(x => x.id_not_mensaje).Column("ID_NOT_MENSAJE").GeneratedBy.Guid();

            Map(x => x.mensaje).Column("MENSAJE");
            Map(x => x.formato).Column("FORMATO");
            Map(x => x.id_not_mensaje_usuario).Column("ID_NOT_MENSAJE_USUARIO");
            Map(x => x.prioridad).Column("PRIORIDAD");
            Map(x => x.nivel).Column("NIVEL");
            Map(x => x.fecha_envio).Column("FECHA_ENVIO");
            Map(x => x.destinatarios).Column("DESTINATARIOS");
            Map(x => x.icono).Column("ICONO");
            Map(x => x.canal_destinatario).Column("CANAL_DESTINATARIO");
            Map(x => x.id_categoria).Column("ID_CATEGORIA");
            Map(x => x.id_aplicacion).Column("ID_APLICACION");
            Map(x => x.asunto).Column("ASUNTO");
        }
    }
}
