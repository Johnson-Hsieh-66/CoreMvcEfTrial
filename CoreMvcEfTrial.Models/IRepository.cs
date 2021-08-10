using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreMvcEfTrial.Models
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        void Create(TEntity cEntity);
        void Create(IEnumerable<TEntity> cEntities);
        TEntity Get(object id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> GetAllQuerable();
        void Update(TEntity uEntity);
        void Update(IEnumerable<TEntity> cEntities);
        void Delete(TEntity dEntity);
        void Delete(object id);
        void ExcuteCommand(string strSqlCommand, object[] oParameters);
    }
}
