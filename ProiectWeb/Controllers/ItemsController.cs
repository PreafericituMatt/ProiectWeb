using Microsoft.AspNetCore.Mvc;

namespace ProiectWeb.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
