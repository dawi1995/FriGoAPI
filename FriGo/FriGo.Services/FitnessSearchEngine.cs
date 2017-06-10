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
       
        public IEnumerable<IngredientQuantity> RawData { get; private set; }
        public IEnumerable<Recipe> RawRecipeData { get; private set; }
        public IEnumerable<KeyValuePair<Recipe, int>> ProcessedData { get; private set; }

        public FitnessSearchEngine(IEnumerable<IngredientQuantity> quantities)
        {
            RawData = quantities;
        }

        public int CalculateFitness(Recipe recipe)
        {
            double fitness = recipe.IngredientQuantities
            .Select(recipeIQ =>
            {
                decimal fridgeQuantity = RawData
                    .Where(x => x.Ingredient.Id == recipeIQ.Ingredient.Id)
                    .Sum(x => x.Quantity);
                return Math.Min(1,(double)fridgeQuantity / (double)recipeIQ.Quantity);
            }).Sum() / recipe.IngredientQuantities.Count * 100;
            return (int)fitness;
        }
        public void SortByFitness(int fitness)
        {
            ProcessedData = RawRecipeData
                .Select(recipe => {
                    return new KeyValuePair<Recipe, int>(recipe, CalculateFitness(recipe));
                })
                .OrderBy(keyValue => keyValue.Value)
                .Where(keyValue => keyValue.Value <= fitness);
        }
    }
}
