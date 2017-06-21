using FriGo.Db.Models;
using FriGo.Db.Models.Recipes;
using FriGo.Interfaces.Dependencies;
using FriGo.DAL;
using FriGo.ServiceInterfaces;
using FriGo.Db.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;

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
            IEnumerable<Rate> rates = rateService.GetByRecipeId(recipe.Id);
            if (rates.Count()>0)
            {
                decimal rating = rating = rates.Select(rate => rate.Rating).Sum() / rates.Count();
                return rating;
            }
            else
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