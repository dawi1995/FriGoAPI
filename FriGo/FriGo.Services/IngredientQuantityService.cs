using System;
using System.Collections.Generic;
using System.Linq;
using FriGo.Db.DAL;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Ingredients;
using FriGo.Interfaces.Dependencies;
using FriGo.ServiceInterfaces;

namespace FriGo.Services
{
    public class IngredientQuantityService : CrudService<IngredientQuantity>, IIngredientQuantityService, IRequestDependency
    {
        public IngredientQuantityService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<IngredientQuantity> GetByUserId(string userId)
        {
            User user = UnitOfWork.Repository<User>().GetById(userId);

            return user?.IngredientQuantities;
        }

        public IngredientQuantity Get(string userId, Guid id)
        {
            IEnumerable<IngredientQuantity> ingredientQuantities = GetByUserId(userId);

            return ingredientQuantities.Single(ingredientQuantity => ingredientQuantity.Id == id);
        }
    }
}