using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using SondaMQ.AdministradorModeloMensaje.Model;
namespace SondaMQ.AdministradorModeloMensaje.Mapping
{
    class CanalMap : ClassMap<NOT_CANAL>
    {
        CanalMap()
        {
            Table("NOT_CANAL");
            Not.LazyLoad();
            Id(x => x.codigo_not_canal).Column("CODIGO_NOT_CANAL").GeneratedBy.Assigned();

            Map(x => x.url).Column("URL");
            Map(x => x.max_reintentos).Column("MAX_REINTENTOS");
            Map(x => x.tiempo_reintentos).Column("TIEMPO_REINTENTOS");
            Map(x => x.canal_obligatorio).Column("CANAL_OBLIGATORIO");
            Map(x => x.formato).Column("FORMATO");
        }
    }
}
