
using FriGo.Db.Models.Ingredients;
using FriGo.Db.Models.Recipes;
using System.Collections.Generic;

namespace FriGo.ServiceInterfaces
{
    public interface IFitnessSearchEngine
    {
        IEnumerable<IngredientQuantity> RawData { get; }
        
        IEnumerable<Recipe> CalculateFitness(decimal fitness);

    }
}
