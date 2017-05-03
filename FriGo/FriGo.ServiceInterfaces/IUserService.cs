using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriGo.Db.Models.Authentication;
namespace FriGo.ServiceInterfaces
{
    public interface IUserService
    {
        User Get(Guid id);

        IEnumerable<User> Get();
    }
}
