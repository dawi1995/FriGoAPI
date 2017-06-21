using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using FriGo.Db.Models;
using FriGo.Db.Models.Recipes;
using FriGo.Interfaces.Dependencies;
using FriGo.Db.DAL;
using FriGo.ServiceInterfaces;
using FriGo.Db.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FriGo.Services

{
    public class RecipeService : CrudService<Recipe>, IRecipeService, IRequestDependency
    {
        public ISearchEngine Engine { get; set; }
        private readonly IRateService rateService;

        public RecipeService(IUnitOfWork unitOfWork, IRateService rateService) : base (unitOfWork)
        {
            Engine = new SearchEngine(this.Get());
            this.rateService = rateService;
        }

        public void SetDefaultPicture(Recipe recipe)
        {
            if (recipe == null) return;
            if (recipe.Tags == null)
            {
                recipe.ImageId = new Guid(Db.Properties.Resources.MainCourseImageId);
                return;
            }

            IList<string> stringTags = GetStringTags(recipe);
            IList<string> dessertTags = GetDessertTags();
            IList<string> appetizerTags = GetAppetizerTags();

            if (stringTags.Any(tag => dessertTags.Contains(tag)))
                recipe.ImageId = new Guid(Db.Properties.Resources.DessertImageId);
            else if (stringTags.Any(tag => appetizerTags.Contains(tag)))
                recipe.ImageId = new Guid(Db.Properties.Resources.AppetizerImageId);
            else
                recipe.ImageId = new Guid(Db.Properties.Resources.MainCourseImageId);
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
            if (rates.Count() > 0)
            {
                var rateForUser = rates.Where(rate => rate.User.Id == user.Id).First();
                return rateForUser.Rating;
            }
            else
            {
                return null;
            }
            
        private IList<string> GetStringTags(Recipe recipe)
        {
            return recipe?.Tags?.Select(tag => tag.Name).ToList() ?? new List<string>();
        }

        private IList<string> GetDessertTags()
        {
            return Db.Properties.Resources.DessertTags.Split(new[] { Db.Properties.Resources.TagsDelimiter },
                StringSplitOptions.None);
        }

        private IList<string> GetAppetizerTags()
        {
            return Db.Properties.Resources.AppetizerTags.Split(new[] { Db.Properties.Resources.TagsDelimiter },
                StringSplitOptions.None);
        }
    }
}