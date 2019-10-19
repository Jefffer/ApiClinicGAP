using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicGAPDataAcces
{
    public class Repository<TContext> where TContext : DbContext, new()
    {
        TContext context = new TContext();

        public List<T> GetAll<T>() where T : class
        {
            return context.Set<T>().ToList();
        }

        public T GetById<T>(int id) where T : class
        {
            return context.Set<T>().Find(id);
        }

        public void Add<T>(T item) where T : class
        {
            context.Set<T>().Add(item);
            Save();
        }

        public void Update<T>(T item) where T : class
        {
            context.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Delete<T>(T item) where T : class
        {
            context.Set<T>().Remove(item);
            Save();
        }


        private void Save()
        {
            context.SaveChanges();
        }
    }
}
