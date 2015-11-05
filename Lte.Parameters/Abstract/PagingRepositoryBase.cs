using System;
using System.Linq;
using System.Linq.Expressions;
using Abp.Domain.Entities;
using Microsoft.Practices.Unity.Utility;

namespace Lte.Parameters.Abstract
{
    public abstract class PagingRepositoryBase<TEntity> : LightWeightRepositroyBase<TEntity>, IPagingRepository<TEntity> 
        where TEntity : class, IEntity<int>, new()
    {
        public IQueryable<TEntity> Get<TKey>(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, 
            Expression<Func<TEntity, TKey>> sortKeySelector, bool isAsc = true)
        {
            Guard.ArgumentNotNull(predicate, "predicate");
            Guard.ArgumentNotNull(sortKeySelector, "sortKeySelector");
            return isAsc
                ? Entities.Where(predicate)
                    .OrderBy(sortKeySelector)
                    .Skip(pageSize*(pageIndex - 1))
                    .Take(pageSize)
                    .AsQueryable()
                : Entities.Where(predicate)
                    .OrderByDescending(sortKeySelector)
                    .Skip(pageSize*(pageIndex - 1))
                    .Take(pageSize)
                    .AsQueryable();
        }
    }
}
