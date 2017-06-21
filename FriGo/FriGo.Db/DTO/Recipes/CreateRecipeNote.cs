using System;

namespace FriGo.Db.DTO.Recipes
{
    public class CreateRecipeNote
    {
        public Guid RecipeId { get; set; }
        public string Note { get; set; }
    }
}