using RepositoryPattern.DAL;

namespace RepositoryPattern.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<Student> GetAll();
        Student GetById(int studentId);
        void Add(Student student);
        void Update(Student student);
        void Delete(int studentId);
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
        void Save();
    }
}
