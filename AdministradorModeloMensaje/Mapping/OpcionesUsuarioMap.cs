using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using SondaMQ.AdministradorModeloMensaje.Model;

namespace SondaMQ.AdministradorModeloMensaje.Mapping
{
    class OpcionesUsuarioMap : ClassMap<NOT_OPCIONES_USUARIO>
    {
        OpcionesUsuarioMap()
        {
            Table("NOT_OPCIONES_USUARIO");
            Not.LazyLoad();
            Id(x => x.id_not_opciones_usuario).Column("ID_NOT_OPCIONES_USUARIO").GeneratedBy.Guid();

            Map(x => x.id_usuario).Column("ID_USUARIO");
            Map(x => x.configuracion).Column("CONFIGURACION");
        }
    }
}
