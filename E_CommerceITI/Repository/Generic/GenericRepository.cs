using E_CommerceITI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace E_CommerceITI.repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
       
        DbSet<T> Entity = null;

        public ApplicationDbContext Context;

        //Ctors
        public GenericRepository()
        {
            Context = new ApplicationDbContext();
            Entity = Context.Set<T>();
        }

        public GenericRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
            Entity = Context.Set<T>();
        }
        //Implementation
        public IEnumerable<T> GetAll()
        {
            return Entity.ToList();
        }
        public T Get(object Id)
        {
            return Entity.Find(Id);
        }

        public void Add(T t)
        {
            Entity.Add(t);
        }

        public void Delete(object Id)
        {
            Entity.Remove(Entity.Find(Id));
        }
        public void Update(T t)
        {
            Entity.Attach(t);
            Context.Entry(t).State = EntityState.Modified;
        }
        public void Save()
        {
            Context.SaveChanges();
        }

    }
}