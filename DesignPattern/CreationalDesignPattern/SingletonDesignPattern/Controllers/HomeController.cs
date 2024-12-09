using Microsoft.AspNetCore.Mvc;
using SingletonDesignPattern.Models;
using SingletonDesignPattern.Service;
using System.Diagnostics;

namespace SingletonDesignPattern.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult AddData()
        {
           
            return View(); 
        }

        [HttpPost]
        public IActionResult AddOrUpdateData(string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                var sharedDataManager = SharedDataManager.Instance;

                // Add or update the key-value pair
                sharedDataManager.AddOrUpdate(key, value);

                TempData["Message"] = $"Key '{key}' has been added or updated successfully!";
            }
            else
            {
                TempData["Message"] = "Key and value must not be empty.";
            }

            return RedirectToAction("ViewSharedData");
        }

        public IActionResult ViewSharedData()
        {
            var sharedDataManager = SharedDataManager.Instance;

            // Retrieve all shared data
            var allData = sharedDataManager.GetAllData();
            return View("SharedDataView", allData);
        }

        public IActionResult Page1()
        {
            var sharedDataManager = SharedDataManager.Instance;

            return View("AddData");
        }

    }
}
