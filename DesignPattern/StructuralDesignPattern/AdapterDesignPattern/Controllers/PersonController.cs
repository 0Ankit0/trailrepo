using AdapterDesignPattern.Adapter;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using AdapterDesignPattern.Models;

namespace AdapterDesignPattern.Controllers
{
    public class PersonController : Controller
    {

        private readonly IDataTableAdapter _adapter;

        public PersonController(IDataTableAdapter adapter)
        {
            _adapter = adapter;
        }
        public IActionResult Index()
        {
            var people = new List<Person>();
            return View(people);
        }

        [HttpPost]
        public IActionResult ProcessPeople(List<Person> People)
        {
            if (People == null || !People.Any())
            {
                return View("Error", "No data submitted.");
            }

            // Example: Generate a DataTable with all properties
            var dataTable = _adapter.ConvertToDataTable(People);
            var filteredDataTable = _adapter.ConvertToDataTable(People, x => x.Name, x => x.Age);
            // Convert DataTable to string for display
            ViewBag.DataTableAll = _adapter.DataTableToString(dataTable); // For all properties
            ViewBag.DataTableWithKeys = _adapter.DataTableToString(filteredDataTable); // For filtered properties

            return View("Result", People);
        }

        public IActionResult Result()
        {
           var people = Enumerable.Empty<Person>();

            return View(people);
        }

       
    }
}
