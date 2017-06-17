using System;
using System.Collections.Generic;
using FriGo.Db.Models.Ingredients;

namespace FriGo.ServiceInterfaces
{
    public interface IIngredientQuantityService
    {
        IEnumerable<IngredientQuantity> Get();
        IngredientQuantity Get(Guid id);
        IEnumerable<IngredientQuantity> GetByUserId(string userId);
        IngredientQuantity Get(string userId, Guid id);
        void Add(IngredientQuantity ingredient);
        void Edit(IngredientQuantity ingredient);
        void Delete(Guid id);
        bool IsOwnedByUser(string userId, Guid id);
    }
}