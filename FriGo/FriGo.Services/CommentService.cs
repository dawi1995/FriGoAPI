using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriGo.DAL;
using FriGo.Db.Models;
using FriGo.Db.Models.Social;
using FriGo.Interfaces.Dependencies;
using FriGo.ServiceInterfaces;

namespace FriGo.Services
{
    class CommentService : CrudService<Comment>, IRateService, IRequestDependency
    {
        public CommentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IEnumerable<Comment> Get()
        {

            return base.Get();
        }
    }
}
