using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity:class
    {
        private readonly ServiceRequestContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ServiceRequestContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();

        }

        public Type ElementType => _dbSet.AsQueryable().ElementType;

        public Expression Expression => _dbSet.AsQueryable().Expression;

        public IQueryProvider Provider => _dbSet.AsQueryable().Provider;

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(Guid entityId)
        {
            _dbSet.Remove(GetById(entityId));
        }

        public IEnumerable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = _dbSet;
            return query.ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {

            throw new NotImplementedException();
        }

        public TEntity GetById(Guid entityId)
        {
            return _dbContext.Find<TEntity>(entityId);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return ((IEnumerable<TEntity>)_dbSet).GetEnumerator();
        }

        public void Update(TEntity entity)
        {
            if (_dbContext.Entry<TEntity>(entity).State == EntityState.Detached)
            {
                _dbContext.Attach(entity);
            }
            _dbContext.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
