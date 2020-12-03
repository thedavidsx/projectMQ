using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using SondaMQ.AdministradorModeloMensaje.Model;

namespace SondaMQ.AdministradorModeloMensaje.Mapping
{
    class CategoriaMap : ClassMap<NOT_CATEGORIA>
    {
        CategoriaMap()
        {
            Table("NOT_CATEGORIA");
            Not.LazyLoad();
            Id(x => x.id_not_categoria).Column("ID_NOT_CATEGORIA").GeneratedBy.Guid();

            Map(x => x.codigo_not_aplicacion).Column("CODIGO_NOT_APLICACION");
            Map(x => x.codigo).Column("CODIGO");
            Map(x => x.descripcion).Column("DESCRIPCION");
        }
    }
}
