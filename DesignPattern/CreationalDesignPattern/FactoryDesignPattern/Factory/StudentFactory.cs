using FactoryDesignPattern.Factory;

namespace FactoryDesignPattern.DAL
{
    public class StudentFactory : IStudentFactory
    {
        public Student CreateStudent(int? studentId, string firstName, string lastName, DateTime dateOfBirth, string gender)
        {
            if(studentId == null || studentId == 0)
            {
                return new Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    Gender = gender
                };
            }
            else
            {
                return new Student
                {
                    StudentID = studentId.Value,
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    Gender = gender
                };
            }
        }
    }
}
