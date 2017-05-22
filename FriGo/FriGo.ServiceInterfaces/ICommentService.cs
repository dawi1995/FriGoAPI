using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriGo.Db.Models.Social;

namespace FriGo.ServiceInterfaces
{
    public interface ICommentService
    {
        IEnumerable<Comment> Get();
        Comment Get(Guid id);
        void Add(Comment comment);
        void Edit(Comment comment);
        void Delete(Guid id);
    }
}
