using System.Collections.Generic;
using FriGo.Db.DAL;
using FriGo.Db.Models;
using FriGo.Db.Models.Ingredients;
using FriGo.Interfaces.Dependencies;
using FriGo.ServiceInterfaces;

namespace FriGo.Services
{
    public class UnitService : CrudService<Unit>, IUnitService, IRequestDependency
    {
        public UnitService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}