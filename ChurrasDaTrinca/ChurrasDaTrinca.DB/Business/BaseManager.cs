using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChurrasDaTrinca.DB.Database;
using System.Linq.Expressions;

/// <summary>
/// Classe genérica para consultas e modificações em banco
/// </summary>

namespace ChurrasDaTrinca.DB.Business
{
    public class BaseManager<T> where T:class
    {
        private CHURRASEntities context;

        public BaseManager()
        {
            context = new CHURRASEntities();
        }

        public void Insert(T element)
        {
            context.Set<T>().Add(element);
            context.SaveChanges();
        }

        public void Update(T element)
        {
            context.Entry(element).CurrentValues.SetValues(element);
            context.Entry(element).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(T element)
        {
            context.Set<T>().Remove(element);
            context.SaveChanges();
        }

        public List<T> GetList(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().Where(match).ToList();
        }

        public T Get(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().FirstOrDefault(match);
        }
    }
}
