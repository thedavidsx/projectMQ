using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using SondaMQ.AdministradorModeloMensaje.Model;

namespace SondaMQ.AdministradorModeloMensaje.Mapping
{
    class AplicacionMap : ClassMap<NOT_APLICACION>
    {
        AplicacionMap()
        {
            Table("NOT_APLICACION");
            Not.LazyLoad();
            Id(x => x.id_not_aplicacion).Column("ID_NOT_APLICACION").GeneratedBy.Guid();

            Map(x => x.codigo).Column("CODIGO");
            Map(x => x.descripcion).Column("DESCRIPCION");
            Map(x => x.obligatorio).Column("OBLIGATORIO");
        }
    }
}
