using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreMvcEfTrial.Models
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private DbContext _context
        {
            get;
            set;
        }
        public GenericRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
        }
        public void Create(TEntity cEntity)
        {
            if (cEntity == null)
            {
                throw new ArgumentNullException("Entity");
            }
            else
            {
                this._context.Set<TEntity>().Add(cEntity);
                this._context.SaveChanges();
            }
        }
        public void Create(IEnumerable<TEntity> cEntities)
        {
            if (cEntities == null || cEntities.Count() == 0)
            {
                throw new ArgumentNullException("Entity");
            }
            else
            {
                this._context.Set<TEntity>().AddRange(cEntities);
                this._context.SaveChanges();
            }
        }
        public void Delete(TEntity dEntity)
        {
            if (dEntity == null)
            {
                throw new ArgumentNullException("Entity");
            }
            else
            {
                if (_context.Entry(dEntity).State == EntityState.Detached)
                {
                    _context.Set<TEntity>().Attach(dEntity);
                }
                _context.Set<TEntity>().Remove(dEntity);
                this._context.SaveChanges();
            }
        }
        public void Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Entity");
            }
            else
            {
                TEntity dEntity = _context.Set<TEntity>().Find(id);
                Delete(dEntity);
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public TEntity Get(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this._context.Set<TEntity>().AsQueryable();
        }
        public IQueryable<TEntity> GetAllQuerable()
        {
            return this._context.Set<TEntity>().AsQueryable();
        }
        public void Update(TEntity uEntity)
        {
            if (uEntity == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(uEntity).State = EntityState.Modified;
                this._context.SaveChanges();
            }
        }
        public void Update(IEnumerable<TEntity> uEntities)
        {
            if (uEntities == null || uEntities.Count() == 0)
            {
                throw new ArgumentNullException("Entity");
            }
            else
            {
                uEntities.ToList().ForEach(e => this._context.Entry(e).State = EntityState.Modified);
                this._context.SaveChanges();
            }
        }
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                    this._context = null;
                }
            }
        }
        public void ExcuteCommand(string strSqlCommand, object[] oParameters)
        {
            if (!string.IsNullOrEmpty(strSqlCommand))
            {
                _context.Database.ExecuteSqlRaw(strSqlCommand, oParameters);
            }
            else
            {
                throw new ArgumentNullException("instance");
            }
        }
    }
}
