using System;
using System.Collections.Generic;
using FriGo.Db.Models.Recipes;
using FriGo.Db.Models.Ingredients;

namespace FriGo.ServiceInterfaces
{
    public interface IFitnessService
    {
        IFitnessSearchEngine EngineFitness { get; set; }
        IEnumerable<IngredientQuantity> Get();
        IngredientQuantity Get(Guid id);
        void Add(IngredientQuantity quantity);
        void Edit(IngredientQuantity quantity);
        void Delete(Guid id);
    }
}
