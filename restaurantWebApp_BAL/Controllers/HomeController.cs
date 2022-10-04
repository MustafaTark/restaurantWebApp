using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurantWebApp_DAL.Data;
using restaurantWebApp_DAL.Models;
using restaurantWebApp_BAL.ViewModels;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace restaurantWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
        {
            _logger = logger;
            _db= db;
        }

        public IActionResult Index()
        {
            var categories=_db.Categories!.Include(m=>m.Meals).Take(4).ToList();
            var meals=_db.Meals!.Include(c=>c.Category).Take(4).ToList();
            var homeVm = new HomeViewModel
            {
                Categories = categories,
                Meals = meals
            };
            return View(homeVm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}