using FriGo.Db.DAL;
using FriGo.Db.Models.Recipes;
using FriGo.Interfaces.Dependencies;
using FriGo.ServiceInterfaces;

namespace FriGo.Services
{
    public class TagService : CrudService<Tag>, ITagService, IRequestDependency
    {
        public TagService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
