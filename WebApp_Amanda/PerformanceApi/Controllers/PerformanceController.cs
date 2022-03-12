using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebService.Model;

namespace PerformanceApi.Controllers
{
    //[Route("api/[controller]/[Action]")]
    //[ApiController]
    public class PerformanceController : ControllerBase
    {
        [HttpGet]
        public ActionResult<object> PerformanceTest()
        {
            var result = new
            {
                Data = "ProxyTest"
            };
            return result;
        }
       
        [HttpGet]
        public ActionResult<PerformanceSetting> GetPerformance()
        {
            var result = new
            {
                Data = "ProxyTest"
            };
            return result;
        }
    }
}
