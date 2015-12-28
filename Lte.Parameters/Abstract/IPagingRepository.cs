using System;
using System.Linq;
using System.Linq.Expressions;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Lte.Parameters.Abstract
{
    public interface IPagingRepository<TEntity> : IRepository<TEntity> 
        where TEntity : class, IEntity<int>
    {
        IQueryable<TEntity> Get<TKey>(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize,
            Expression<Func<TEntity, TKey>> sortKeySelector, bool isAsc = true);

        IQueryable<TEntity> GetAll<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> sortKeySelector,
            bool isAsc = true);
    }
}
