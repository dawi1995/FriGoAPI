using System;

namespace FriGo.Db.ModelValidators.Interfaces
{
    public interface IAbstractDatabaseValidator
    {
        bool EntityExists<TDatabaseEntity>(Guid entityId) where TDatabaseEntity : class;
    }
}