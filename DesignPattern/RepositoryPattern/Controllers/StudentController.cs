using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RepositoryPattern.DAL;
using RepositoryPattern.Repository;

namespace RepositoryPattern.Controllers
{
    public class StudentController : Controller
    {
		private readonly IStudentRepository<Student> _studentRepository;

		// Constructor injection via DI
		public StudentController(IStudentRepository<Student> studentRepository)
		{
			_studentRepository = studentRepository;
		}

		// GET: Students
		public async Task<IActionResult> Index()
        {
            var students = _studentRepository.GetAll();
            return View(students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentRepository.GetById(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,FirstName,LastName,DateOfBirth,Gender")] Student student)
        {
            if (ModelState.IsValid)
            {
                _studentRepository.Add(student);
                _studentRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentRepository.GetById(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("StudentID,FirstName,LastName,DateOfBirth,Gender")] Student student)
        {
            if (student.StudentID== 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _studentRepository.Update(student);
                    _studentRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentRepository.GetById(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _studentRepository.Delete(id);
            _studentRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _studentRepository.GetAll().Any(e => e.StudentID == id);
        }
    }
}
