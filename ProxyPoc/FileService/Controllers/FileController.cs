using Microsoft.AspNetCore.Mvc;

namespace FileService.Controllers
{
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public FileController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public FileResult GetProxySetting()
        {
            var filePath = Path.Combine(_env.ContentRootPath, "Config/ProxySetting.json");
            return PhysicalFile(filePath, "application/json");
        }
    }
}
