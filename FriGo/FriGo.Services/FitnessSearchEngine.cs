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
        public IEnumerable<KeyValuePair<Recipe, decimal>> ProcessedData { get; private set; }
        const decimal maxFitnessSufficient = 1;

        public FitnessSearchEngine(IEnumerable<IngredientQuantity> quantities)
        {
            RawData = quantities;
        }

        public decimal CalculateFridgeFitness(IngredientQuantity quantities)
        {
            decimal fridgeQuantity = RawData
                    .Where(x => x.Ingredient.Id == quantities.Ingredient.Id)
                    .Sum(x => x.Quantity);
            return fridgeQuantity;
        }

        public decimal CheckForSufficiency(decimal fridgeQuantity, IngredientQuantity quantities)
        {
            return Math.Min(maxFitnessSufficient, fridgeQuantity / quantities.Quantity);
        }
        public decimal CalculateFitness(Recipe recipe)
        {
            decimal fitness = recipe.IngredientQuantities
            .Select(recipeIQ =>
                CheckForSufficiency(CalculateFridgeFitness(recipeIQ), recipeIQ))
                .Sum() / recipe.IngredientQuantities.Count;
            return fitness;
        }
        public void SortByFitness(decimal fitness)
        {
            ProcessedData = RawRecipeData
                .Select(recipe =>
                {
                    return new KeyValuePair<Recipe, decimal>(recipe, CalculateFitness(recipe));
                })
                .OrderBy(keyValue => keyValue.Value)
                .Where(keyValue => keyValue.Value <= fitness);
        }
    }
}
