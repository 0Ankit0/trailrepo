using RepositoryPattern.DAL;
namespace RepositoryPattern.Repository
{
    public interface IStudentRepository<T>:IGenericRepository<T> where T : class
    {

        string GetName(int id);
    }
}
