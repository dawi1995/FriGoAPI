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
        private IEnumerable<IngredientQuantity> enumerable;

        public IEnumerable<IngredientQuantity> RawData { get; private set; }
        public IEnumerable<Recipe> RawRecipeData { get; private set; }

        public FitnessSearchEngine(IEnumerable<IngredientQuantity> quantities)
        {
            RawData = quantities;
        }


        public decimal UserFitnessStatus()
        {
            return RawData.Sum(x => x.Quantity);
        }
        public decimal RecipeFitnessStatus()
        {
            return RawRecipeData.Sum(x => x.IngredientQuantities.Sum(z => z.Quantity));
        }
        public decimal Fitness()
        {
            return (UserFitnessStatus() / RecipeFitnessStatus()) * 100;
        }
        public void SortByFitness(decimal fitness)
        {
            //do implementacji
        }
    }
}
