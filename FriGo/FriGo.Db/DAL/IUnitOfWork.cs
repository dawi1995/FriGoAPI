namespace FriGo.Db.DAL
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;
        void Save();
    }
}