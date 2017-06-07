
using FriGo.Db.Models.Ingredients;
using System.Collections.Generic;

namespace FriGo.ServiceInterfaces
{
    public interface IFitnessSearchEngine
    {
        IEnumerable<IngredientQuantity> RawData { get; }
        decimal UserFitnessStatus();
        decimal RecipeFitnessStatus();
        decimal Fitness();
        void SortByFitness(decimal fitness);

    }
}
