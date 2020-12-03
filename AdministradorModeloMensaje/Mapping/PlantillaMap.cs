using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using SondaMQ.AdministradorModeloMensaje.Model;

namespace SondaMQ.AdministradorModeloMensaje.Mapping
{
    class PlantillaMap : ClassMap<NOT_PLANTILLA>
    {
        PlantillaMap()
        {
            Table("NOT_PLANTILLA");
            Not.LazyLoad();
            Id(x => x.id_not_plantilla).Column("ID_NOT_PLANTILLA").GeneratedBy.Guid();

            Map(x => x.id_not_canal).Column("ID_NOT_CANAL");
            Map(x => x.formato).Column("FORMATO");
            Map(x => x.contenido).Column("CONTENIDO");
        }
    }
}
