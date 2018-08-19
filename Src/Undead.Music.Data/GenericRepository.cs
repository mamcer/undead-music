using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Undead.Music.Data
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private readonly IEntities _context;
        private readonly DbSet<TEntity> _dbset;

        public GenericRepository(IEntities context)
        {
            _context = context;
            _dbset = _context.GetSet<TEntity>();
        }

        public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            // sample call: .Search(m => m.Status == MentorStatus.Active, q => q.OrderByDescending(p => p.CreationDate), "Topic,User"); 

            var query = CreateQuery(filter, orderBy, includeProperties);

            return query.ToList();
        }

        public TEntity Get(int id)
        {
            return _dbset.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbset.ToList();
        }

        public void Insert(TEntity entity)
        {
            _dbset.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (_context.GetState(entity) == EntityState.Detached)
            {
                _dbset.Attach(entity);
            }

            _dbset.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbset.Attach(entity);
            _context.SetModified(entity);
        }

        public int Count()
        {
            return _dbset.Count();
        }

        private IQueryable<TEntity> CreateQuery(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties)
        {
            IQueryable<TEntity> query = _dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }
    }
}