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


        public decimal CalculateUserFitness()
        {
            return RawData.Sum(x => x.Quantity);
        }
        public decimal CalculateRecipeFitness()
        {
            return RawRecipeData.Sum(x => x.IngredientQuantities.Sum(z => z.Quantity));
        }
        public decimal CalculateFitness()
        {
            return (CalculateUserFitness() / CalculateRecipeFitness()) * 100;
        }
        public void SortByFitness(decimal fitness)
        {
            //do implementacji
        }
    }
}
