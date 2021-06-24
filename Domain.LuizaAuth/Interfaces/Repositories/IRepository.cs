using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.LuizaAuth.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Create(TEntity model);

        List<TEntity> Create(List<TEntity> model);

        int Save();

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> where);        

        Task<TEntity> CreateAsync(TEntity model);

        Task<int> SaveAsync();

        Task<TEntity> GetAsync(params object[] Keys);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);

        Task<bool> UpdateAsync(TEntity model);
    }

}
