using FactoryDesignPattern.DAL;

namespace FactoryDesignPattern.Factory
{
    public interface IStudentFactory
    {
        Student CreateStudent(int? studentId, string firstName, string lastName, DateTime dateOfBirth, string gender);
    }
}
