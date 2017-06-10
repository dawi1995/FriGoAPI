using System;
using System.Collections.Generic;
using FriGo.Db.Models.Recipes;
using FriGo.ServiceInterfaces;
using System.Linq;
using FriGo.Db.Models.Ingredients;

namespace FriGo.Services
{
    public class FitnessSearchEngine : IFitnessSearchEngine
    {
        private IEnumerable<IngredientQuantity> quantities;

        public IEnumerable<IngredientQuantity> RawData { get; private set; }
        public IEnumerable<Recipe> RawRecipeData { get; private set; }

        public FitnessSearchEngine(IEnumerable<IngredientQuantity> quantities)
        {
            RawData = quantities;
        }

        public IEnumerable<Recipe> CalculateFitness(decimal minFitness)
        {
            IEnumerable<Recipe> fittingRecipes = RawRecipeData.Select(r=> 
            {
                decimal fitness = r.IngredientQuantities.Select(recipeIQ =>
                {
                    decimal fridgeQ = RawData
                    .Where(fridgeItem => fridgeItem.Ingredient.Id == recipeIQ.Ingredient.Id)
                    .Sum(x => x.Quantity);
                    return Math.Min(1, fridgeQ / recipeIQ.Quantity);
                }).Sum() / r.IngredientQuantities.Count * 100;
                return new { Fitness = fitness, Recipe = r };
            }).Where(fr => fr.Fitness >= minFitness)
            .Select(fr => fr.Recipe);
            return fittingRecipes;
        }

        public IEnumerable<Recipe> SortByFitness(decimal fitness)
        {
            throw new NotImplementedException();
        }
    }
}
