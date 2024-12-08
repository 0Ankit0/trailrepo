using RepositoryPattern.DAL;

namespace RepositoryPattern.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
        Task Add(T data);
        Task Update(T id);
        Task Delete(int id);
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
        void Save();
    }
}
