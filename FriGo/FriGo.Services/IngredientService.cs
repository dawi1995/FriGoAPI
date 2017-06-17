using System.Collections.Generic;
using FriGo.Db.DAL;
using FriGo.Db.Models;
using FriGo.Db.Models.Ingredients;
using FriGo.Interfaces.Dependencies;
using FriGo.ServiceInterfaces;

namespace FriGo.Services
{
    public class IngredientService : CrudService<Ingredient>, IIngredientService, IRequestDependency
    {
        public IngredientService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IEnumerable<Ingredient> Get()
        {

            return base.Get();
        }
    }
}