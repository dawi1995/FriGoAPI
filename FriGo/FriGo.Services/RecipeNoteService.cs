using System;
using System.Collections.Generic;
using System.Linq;
using FriGo.Db.DAL;
using FriGo.Db.Models;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.Models.Recipes;
using FriGo.Interfaces.Dependencies;
using FriGo.ServiceInterfaces;

namespace FriGo.Services
{
    public class RecipeNoteService : CrudService<RecipeNote>, IRecipeNoteService, IRequestDependency
    {
        public RecipeNoteService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<RecipeNote> Get(Guid recipeId, Guid userId)
        {
            return UnitOfWork.Repository<RecipeNote>()
                .Get(recipeNote =>
                    recipeNote.UserId == userId &&
                    recipeNote.RecipeId == recipeId);
        }
    }
}