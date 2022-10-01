using Microsoft.AspNetCore.Mvc;

namespace restaurantWebApp.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
