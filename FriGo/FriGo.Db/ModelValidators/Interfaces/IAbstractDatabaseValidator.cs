using System;
using FluentValidation;

namespace FriGo.Db.ModelValidators.Interfaces
{
    public interface IAbstractDatabaseValidator : IValidator
    {
        bool EntityExists<TDatabaseEntity>(Guid entityId) where TDatabaseEntity : class;
    }
}