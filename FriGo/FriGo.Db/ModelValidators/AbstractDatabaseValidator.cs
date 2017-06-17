using System;
using FluentValidation;
using FriGo.Db.DAL;

namespace FriGo.Db.ModelValidators
{
    public class AbstractDatabaseValidator<TValidatedEntity> : AbstractValidator<TValidatedEntity>
    {
        private readonly IUnitOfWork unitOfWork;

        public AbstractDatabaseValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool UniqueEntity<TDatabaseEntity>(Guid entityId) where TDatabaseEntity : class
        {
            TDatabaseEntity entity = unitOfWork.Repository<TDatabaseEntity>().GetById(entityId);

            return entity != null;
        }
    }
}