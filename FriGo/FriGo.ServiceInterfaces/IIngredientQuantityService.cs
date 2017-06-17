using System;
using System.Collections.Generic;
using FriGo.Db.Models.Ingredients;

namespace FriGo.ServiceInterfaces
{
    public interface IIngredientQuantityService
    {
        IEnumerable<IngredientQuantity> Get();
        IEnumerable<IngredientQuantity> GetByUserId(string userId);
        IngredientQuantity Get(string userId, Guid id);
        void Add(IngredientQuantity ingredient);
        void Edit(IngredientQuantity ingredient);
        void Delete(Guid id);
    }
}