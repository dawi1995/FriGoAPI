using System.Collections.Generic;
using FriGo.Db.Models.Recipes;

namespace FriGo.ServiceInterfaces
{
    public interface ITagService
    {
        IEnumerable<Tag> Get();
    }
}
