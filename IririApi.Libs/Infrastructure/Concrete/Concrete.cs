using IririApi.Libs.Infrastructure.Contract;
using IririApi.Libs.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IririApi.Libs.Infrastructure.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //private readonly DbContext _context;
        private readonly AuthenticationContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AuthenticationContext Db)
        {
            if (Db == null) throw new ArgumentNullException("Db");
            _context = Db;
            _dbSet = Db.Set<T>();
        }

        public virtual T Add(T entity)
        {
            dynamic entityAdded = _dbSet.Add(entity);
            _context.SaveChanges();
            return entityAdded;
        }

        //public virtual T Add(T entity, T entity2)
        //{
        //    dynamic entityAdded = _dbSet.Add(entity,entity2);
        //    _context.SaveChanges();
        //    return entityAdded;
        //}



        public virtual T Remove(T entity)
        {
            dynamic entityRemoved = _dbSet.Remove(entity);
            _context.SaveChanges();
            return entityRemoved;
        }

        public virtual T Remove(object key)
        {
            var entity = _dbSet.Find(key);
            dynamic entityRemoved = _dbSet.Remove(entity);
            _context.SaveChanges();
            return entityRemoved;
        }

        public virtual T Update(T entity)
        {
            dynamic updated = _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return updated;
        }

        public IEnumerable<T> UpdateRange(List<T> entities)
        {
            var retVals = new List<T>();
            foreach (var item in entities)
            {
                dynamic updated = _dbSet.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
                retVals.Add(updated);
            }
            _context.SaveChanges();
            return retVals;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public T GetById(object key)
        {
            return _dbSet.Find(key);
        }

        public T GetByCustomerId(object key)
        {
            return _dbSet.Find(key);
        }

    }
}
