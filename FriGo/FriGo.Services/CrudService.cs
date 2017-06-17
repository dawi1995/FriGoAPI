using System;
using System.Collections.Generic;
using FriGo.Db.DAL;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Services
{
    public abstract class CrudService<TEntity> : BaseService, IRequestDependency where TEntity : class 
    {
        protected CrudService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public virtual IEnumerable<TEntity> Get()
        {
            return UnitOfWork.Repository<TEntity>().GetAll();
        }

        public virtual TEntity Get(Guid id)
        {
            return UnitOfWork.Repository<TEntity>().GetById(id);
        }

        public virtual void Add(TEntity ingredient)
        {
            UnitOfWork.Repository<TEntity>().Insert(ingredient);

            UnitOfWork.Save();
        }

        public virtual void Edit(TEntity ingredient)
        {
            UnitOfWork.Repository<TEntity>().Edit(ingredient);

            UnitOfWork.Save();
        }

        public virtual void Delete(Guid id)
        {
            UnitOfWork.Repository<TEntity>().Delete(id);

            UnitOfWork.Save();
        }
    }
}