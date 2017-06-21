using System;
using System.Collections.Generic;
using FriGo.Db.Models.Recipes;
using FriGo.Db.Models.Authentication;

namespace FriGo.ServiceInterfaces
{
    public interface IRecipeService
    {
        ISearchEngine Engine { get; set; }
        IEnumerable<Recipe> Get();
        Recipe Get(Guid id);
        void Add(Recipe recipe);
        void Edit(Recipe recipe);
        void Delete(Guid id);
        decimal? GetRatingByRecipe(Recipe recipe);
        decimal? GetRatingByUser(User user, Recipe recipe);
        void SetDefaultPicture(Recipe recipe);
    }
}