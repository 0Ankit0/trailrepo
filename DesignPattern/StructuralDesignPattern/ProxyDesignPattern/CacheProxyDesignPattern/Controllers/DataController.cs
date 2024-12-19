using CacheProxyDesignPattern.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;

namespace CacheProxyDesignPattern.Controllers
{
    public class DataController : Controller
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }
        public async Task<IActionResult> Index(string key="key")
        {
            var data = await _dataService.GetDataAsync(key);
            return View(data);
        }
    }
}
