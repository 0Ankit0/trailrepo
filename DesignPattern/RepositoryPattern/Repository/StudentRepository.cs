using RepositoryPattern.DAL;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPattern.Repository
{
	public class StudentRepository<T> : BaseRepository<T> where T : class
	{
		private readonly StudentContext _context;

		public StudentRepository(StudentContext context) : base(context)
        {
            _context = context;
        }

	}
}
