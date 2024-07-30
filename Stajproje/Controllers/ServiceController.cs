using Microsoft.AspNetCore.Mvc;

namespace Stajproje.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
