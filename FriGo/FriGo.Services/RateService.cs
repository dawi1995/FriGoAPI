using FriGo.DAL;
using FriGo.Db.Models.Recipes;
using FriGo.Interfaces.Dependencies;
using FriGo.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriGo.Services
{
    class RateService : CrudService<Rate>, IRateService, IRequestDependency
    {
        public RateService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IEnumerable<Rate> Get()
        {
            return base.Get();
        }

        public IEnumerable<Rate> GetByRecipeId(Guid recipeId)
        {
            var rates = UnitOfWork.Repository<Rate>().Get(rate => rate.Recipe.Id == recipeId);
            return rates;
        }

        public IEnumerable<Rate> GetByUserId(Guid userId)
        {
            var rates = UnitOfWork.Repository<Rate>().Get(rate => rate.User.Id == userId.ToString());
            return rates;
        }
    }
}
