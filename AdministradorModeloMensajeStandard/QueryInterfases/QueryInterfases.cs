using System;
using System.Collections.Generic;
using System.Text;

namespace SondaMQ.AdministradorModeloMensaje.QueryInterfases
{
    interface QueryInterfases<T>
    {
        T Create(T objeto);
        T Read(int id);
        T Read(Guid id);
        T Read(String id);
        //T ReadDateTime(DateTime id);
        void Update(String filtro,T objeto);
        void Update(int filtro, T objeto);
        void Update(Guid filtro, T objeto);
        void Delete(int id);
        void Delete(Guid id);
        void Delete(string id);
        //void DeleteDateTime(DateTime id);
        IEnumerable<T> AllRead();
    }
}
