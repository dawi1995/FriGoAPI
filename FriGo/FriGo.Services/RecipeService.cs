using FriGo.Db.Models;
using FriGo.Db.Models.Recipes;
using FriGo.Interfaces.Dependencies;
using FriGo.Db.DAL;
using FriGo.ServiceInterfaces;

namespace FriGo.Services

{
    public class RecipeService : CrudService<Recipe>, IRecipeService, IRequestDependency
    {
        public ISearchEngine Engine { get; set; }

        public void SetDefaultPicture(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }

        public RecipeService(IUnitOfWork unitOfWork) : base (unitOfWork)
        {
            Engine = new SearchEngine(this.Get());
        }

        
    }
}