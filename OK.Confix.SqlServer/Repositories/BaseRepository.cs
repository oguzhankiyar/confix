using OK.Confix.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace OK.Confix.SqlServer.Repositories
{
    public class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext dataContext;

        private DbSet<TEntity> Entities
        {
            get
            {
                return dataContext.Set<TEntity>();
            }
        }

        public BaseRepository(DbContext dataContext)
        {
            this.dataContext = dataContext;
        }


        protected IEnumerable<TEntity> GetList()
        {
            return Entities.Where(x => x.IsDeleted == false);
        }

        protected IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(x => x.IsDeleted == false).Where(predicate);
        }

        protected virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(x => x.IsDeleted == false).FirstOrDefault(predicate);
        }

        protected virtual TEntity GetById(int id)
        {
            return Entities.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
        }

        protected virtual TEntity Create(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            return Entities.Add(entity);
        }

        protected virtual void Edit(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        protected virtual void Delete(TEntity entity)
        {
            entity = GetById(entity.Id);
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        protected int SaveChanges()
        {
            return dataContext.SaveChanges();
        }
    }
}