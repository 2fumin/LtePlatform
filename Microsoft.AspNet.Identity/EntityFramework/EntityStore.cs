using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    internal class EntityStore<TEntity> where TEntity : class
    {
        public EntityStore(DbContext context)
        {
            Context = context;
            DbEntitySet = context.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            DbEntitySet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            DbEntitySet.Remove(entity);
        }

        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            return DbEntitySet.FindAsync(id);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity != null)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public DbContext Context { get; }

        public DbSet<TEntity> DbEntitySet { get; }

        public IQueryable<TEntity> EntitySet => DbEntitySet;
    }
}
