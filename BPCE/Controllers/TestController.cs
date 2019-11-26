using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BPCE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BPCE.Controllers
{
    public class TestController : Controller
    {
        private List<Test> listItem;
        private IMemoryCache _cache;
        public TestController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        public IActionResult Index()
        {
            if (!_cache.TryGetValue("_cache", out listItem))
            {
                listItem = new List<Test>();
            }
            return View(listItem);
        }
        public IActionResult Create()
        {
            if (!_cache.TryGetValue("_cache", out listItem))
            {
                listItem = new List<Test>();
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Test test)
        {
            
                if (!_cache.TryGetValue("_cache", out listItem))
            {
                listItem = new List<Test>();
            }
            listItem.Add(test);
            _cache.Set("_cache", listItem);
            return RedirectToAction("Index");
        }

       
        [HttpPost]
        public IActionResult Filtrer(int min , int max)
        {

            if (!_cache.TryGetValue("_cache", out listItem))
            {
                listItem = new List<Test>();
            }
            var listItemFilter = listItem.Where(x => x.Id >= min && x.Id <= max).ToList();
            return PartialView("_Filtrer", listItemFilter);
        }
    }
}