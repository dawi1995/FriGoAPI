using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.Models.Recipes;
using FriGo.Db.Models.Social;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FriGo.Db.Models.Authentication
{
    public class User : IdentityUser
    {
        public override string Id
        {
            get
            {
                return base.Id;
            }

            set
            {
                base.Id = value;
            }
        }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<IngredientQuantity> IngredientQuantities { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
    
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}