using FriGo.Db.DTO.Recipes;
using FriGo.Db.Models.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriGo.ServiceInterfaces
{
    public interface IRateService
    {
        IEnumerable<Rate> Get();
        Rate Get(Guid id);

        IEnumerable<Rate> GetByUserId(string userId);
        IEnumerable<Rate> GetByRecipeId(Guid recipeId);
        void Add(Rate rate);
        void Edit(Rate rate);
        void Delete(Guid id);
    }
}
