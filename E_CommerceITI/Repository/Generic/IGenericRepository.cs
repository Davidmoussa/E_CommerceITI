using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceITI.repository
{
    public interface IGenericRepository<T> where T : class 
    {
        IEnumerable<T> GetAll();
        T Get(object Id);
        void Add(T t);
        void Update(T t);
        void Delete(object Id);
        void Save();

    }
}