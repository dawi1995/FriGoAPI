using System;
using System.Collections.Generic;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.Models.Recipes;

namespace FriGo.ServiceInterfaces
{
    public interface IRecipeNoteService
    {
        IEnumerable<RecipeNote> Get();
        RecipeNote Get(Guid recipeNoteId);
        IEnumerable<RecipeNote> Get(Guid recipeId, Guid userId);
        void Add(RecipeNote recipeNote);
        void Edit(RecipeNote recipeNote);
        void Delete(Guid id);
    }
}