using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;
using FriGo.Db.DAL;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Db.ModelValidators.Validators
{
    public class AbstractDatabaseValidator<TValidatedEntity> : AbstractValidator<TValidatedEntity>, IAbstractDatabaseValidator, IRequestDependency
    {
        private readonly IUnitOfWork unitOfWork;

        public AbstractDatabaseValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public AbstractDatabaseValidator()
        {
        }

        public bool EntityExists<TDatabaseEntity>(Guid entityId) where TDatabaseEntity : class
        {
            TDatabaseEntity entity = unitOfWork.Repository<TDatabaseEntity>().GetById(entityId);

            return entity != null;
        }

        public bool IsFieldUnique<TDatabaseEntity>(Expression<Func<TDatabaseEntity, bool>> databasePredicate) where TDatabaseEntity : class
        {
            IEnumerable<TDatabaseEntity> entities = unitOfWork.Repository<TDatabaseEntity>().Get(databasePredicate);

            return !entities.Any();
        }
    }
}