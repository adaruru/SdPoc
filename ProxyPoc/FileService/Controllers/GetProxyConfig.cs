using Microsoft.AspNetCore.Mvc;

namespace FileService.Controllers
{
    public class GetProxyConfig : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
