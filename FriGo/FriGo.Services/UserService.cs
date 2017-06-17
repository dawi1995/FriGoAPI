using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriGo.Services;
using FriGo.Db.Models.Social;
using FriGo.Db.Models.Ingredients;
using FriGo.Interfaces.Dependencies;
using FriGo.ServiceInterfaces;
using FriGo.Db.DAL;
using FriGo.Db.Models.Authentication;

namespace FriGo.Services
{
    public class UserService : CrudService<User>, IUserService, IRequestDependency
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public User Get(string id)
        {
            return UnitOfWork.Repository<User>().GetById(id);
        }
    }
}
