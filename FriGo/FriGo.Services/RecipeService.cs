using FriGo.Db.Models;
using FriGo.Db.Models.Recipes;
using FriGo.Interfaces.Dependencies;
using FriGo.DAL;
using FriGo.ServiceInterfaces;
using FriGo.Db.Models.Authentication;
using System;

namespace FriGo.Services

{
    public class RecipeService : CrudService<Recipe>, IRecipeService, IRequestDependency
    {//Add dependency
        public ISearchEngine Engine { get; set; }
        private readonly IRateService rateService;

        public RecipeService(IUnitOfWork unitOfWork, IRateService rateService) : base (unitOfWork)
        {
            Engine = new SearchEngine(this.Get());
            this.rateService = rateService;
        }

        public decimal? GetRatingByRecipe(Recipe recipe)
        {
            try
            {
                var rates = rateService.GetByRecipeId(recipe.Id);

                decimal rating = 0;
                decimal sum = 0;
                decimal count = 0;
                foreach (var rate in rates)
                {
                    count++;
                    sum += rate.Rating;
                }
                rating = sum / count;
                return rating;
            }
            catch (Exception)
            {

                return null;
            }


        }

        decimal? IRecipeService.GetRatingByUser(User user, Recipe recipe)
        {
            var rates = rateService.GetByRecipeId(recipe.Id);
            foreach (var rate in rates)
            {
                if (rate.User.Id == user.Id)
                {
                    return rate.Rating;
                }
            }
            return null;
        }
    }
}