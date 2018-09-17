using ATM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Tests.MockObjects
{
    public class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly List<TEntity> dbSet;


        public FakeRepository()
        {
            dbSet = new List<TEntity>();
        }



        public void Add(TEntity entity)
        {
            dynamic e = entity;
            e.Id = dbSet.Count;
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);

        }

        public int Count()
        {
            return dbSet.Count();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.AsQueryable().Where(predicate);
        }

        public TEntity Get(int id)
        {
            return dbSet.Find(x => { dynamic a = x; return a.Id == id; } );
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                dbSet.Remove(entity);
            }
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.AsQueryable().SingleOrDefault(predicate);
        }
    }

}
