using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.Models.Recipes;

namespace FriGo.Db.EntityConfigurations
{
    public class RecipeNoteConfiguration : EntityTypeConfiguration<RecipeNote>
    {
        public RecipeNoteConfiguration()
        {
            Property(recipeNote => recipeNote.UserId).IsRequired();
            Property(recipeNote => recipeNote.RecipeId).IsRequired();
            Property(recipeNote => recipeNote.Note).IsRequired();
        }
    }
}