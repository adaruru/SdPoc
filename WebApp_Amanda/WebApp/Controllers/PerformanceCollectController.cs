using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class PerformanceCollectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
