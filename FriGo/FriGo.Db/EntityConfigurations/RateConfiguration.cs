using FriGo.Db.Models.Recipes;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriGo.Db.EntityConfigurations
{
    class RateConfiguration : EntityTypeConfiguration<Rate>
    {
        public RateConfiguration()
        {
            HasRequired(rate => rate.User) 
                    .WithMany(user => user.Rates);

            HasRequired(rate => rate.Recipe)
                    .WithMany(recipe => recipe.Rates);
        }
    }
}
