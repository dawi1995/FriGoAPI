using FriGo.Db.Models;
using FriGo.Interfaces.Dependencies;
using FriGo.Db.DAL;
using FriGo.ServiceInterfaces;
using FriGo.Db.Models.Ingredients;

namespace FriGo.Services
{
    public class FitnessService : CrudService<IngredientQuantity>, IFitnessService, IRequestDependency
    {
        public IFitnessSearchEngine EngineFitness { get; set; }
        public FitnessService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            EngineFitness = new FitnessSearchEngine(Get());
        }
    }
}
