
using FriGo.Db.Models.Ingredients;
using System.Collections.Generic;

namespace FriGo.ServiceInterfaces
{
    public interface IFitnessSearchEngine
    {
        IEnumerable<IngredientQuantity> RawData { get; }
        decimal CalculateUserFitness();
        decimal CalculateRecipeFitness();
        decimal CalculateFitness();
        void SortByFitness(decimal fitness);

    }
}
