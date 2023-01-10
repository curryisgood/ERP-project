using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1111.Store
{
   interface IStore<T>
    {
        void Save(T data);
        List<T> GetAll();
        List<T> Get(Predicate<T> match);
        T GetID(Predicate<T> match);
        void Init();//

    }
}