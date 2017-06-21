using System;
using System.Collections.Generic;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.Models.Social;

namespace FriGo.Db.Models.Recipes
{
    public class Recipe : Entity
    {
        public Recipe()
        {
            CreatedAt = DateTime.Now;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? ImageId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<IngredientQuantity> IngredientQuantities { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
