
using FriGo.Db.Models.Ingredients;
using FriGo.Db.Models.Recipes;
using System.Collections.Generic;

namespace FriGo.ServiceInterfaces
{
    public interface IFitnessSearchEngine
    {
        IEnumerable<IngredientQuantity> RawData { get; }
        IEnumerable<Recipe> RawRecipeData { get; }
        IEnumerable<KeyValuePair<Recipe, decimal>> ProcessedData { get; }

        decimal CalculateFitness(Recipe recipe);
        void SortByFitness(decimal fitness);

    }
}
