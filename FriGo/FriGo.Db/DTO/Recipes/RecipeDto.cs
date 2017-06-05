using System;
using System.Collections.Generic;
using FriGo.Db.DTO.Account;
using FriGo.Db.DTO.Social;
using FriGo.Db.Models.Recipes;

namespace FriGo.Db.DTO.Recipes
{
    public class RecipeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Models.Ingredients.IngredientQuantity[] IngredientQuantities { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
        public IEnumerable<Tag> Tags { get; set;}
        public UserStub User { get; set; }
        public string Base64Picture { get; set; }
        public decimal? Rating { get; set; }
        public int? UserRating { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Fitness { get; set; }
        public IEnumerable<Models.Ingredients.IngredientQuantity> MissingIngredientQuantities { get; set; }
    }
}