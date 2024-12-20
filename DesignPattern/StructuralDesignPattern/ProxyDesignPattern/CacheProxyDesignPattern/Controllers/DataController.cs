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
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetData(string key)
        {
            var data = await _dataService.GetDataAsync(key);
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> SetData(string key, string value)
        {
            await _dataService.SetDataAsync(key, value);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteData(string key)
        {
            await _dataService.DeleteDataAsync(key);
            return Ok();
        }
    }
}
