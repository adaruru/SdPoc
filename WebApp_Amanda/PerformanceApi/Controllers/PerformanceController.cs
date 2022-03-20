using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerformanceApi.Lib;
using PerformanceApp.Model;

namespace PerformanceApi.Controllers
{
    public class PerformanceController : ControllerBase
    {
        private readonly IPerformanceApiService _performanceSettingService;

        public PerformanceController(IPerformanceApiService performanceSettingService)
        {
            _performanceSettingService = performanceSettingService;
        }

        //http://localhost:35200/Performance/PerformanceTest
        [HttpGet]
        public ActionResult<object> PerformanceTest()
        {
            var result = new
            {
                Data = "ProxyTest"
            };
            return result;
        }

        //http://localhost:35200/Performance/GetPerformanceSetting
        [HttpGet]
        public ActionResult<PerformanceSetting> GetPerformanceSetting()
        {
            return _performanceSettingService.GetPerformanceSetting();
        }
        
        [HttpPost]
        public ActionResult<PerformanceSetting> UpdatePerformanceSetting()
        {
            return _performanceSettingService.GetPerformanceSetting();
        }
        
        //http://localhost:35200/Performance/GetPerformanceSetting
        [HttpGet]
        public ActionResult<object> GetPerformance()
        {
            return _performanceSettingService.GetPerformance();
        }
    }
}
