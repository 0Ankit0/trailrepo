﻿using RepositoryPattern.DAL;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPattern.Repository
{
	public class StudentRepository<T> : IStudentRepository<T> where T : class
	{
		private readonly StudentContext _context;

		// Constructor injection via DI
		public StudentRepository(StudentContext context)
		{
			_context = context;
		}

		// Get all students
		public IEnumerable<Student> GetAll()
		{
			return _context.Students.ToList();
		}

		// Get student by ID
		public Student GetById(int id)
		{
			return _context.Students.Find(id);
		}

		public string GetName(int id)
		{
            return _context.Students.Find(id).FullName ?? "";
		}

		// Add a new student
		public void Add(Student student)
		{
			_context.Students.Add(student);
		}

		// Update an existing student
		public void Update(Student student)
		{
			_context.Students.Update(student);
		}

		// Delete a student by ID
		public void Delete(int id)
		{
			var student = _context.Students.Find(id);
			if (student != null)
			{
				_context.Students.Remove(student);
			}
		}


		public Task BeginTransaction()
		{
            return _context.Database.BeginTransactionAsync();
		}

		public Task Commit()
        {
            return _context.Database.CommitTransactionAsync();
        }

		public Task Rollback()
		{
            return _context.Database.RollbackTransactionAsync();
		}

		// Save changes to the context
		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
