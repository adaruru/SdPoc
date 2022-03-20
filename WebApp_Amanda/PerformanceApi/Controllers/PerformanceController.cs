using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerformanceApi.Lib;
using PerformanceApp.Lib;
using PerformanceApp.Model;

namespace PerformanceApi.Controllers
{
    public class PerformanceController : ControllerBase
    {
        private readonly IPerformanceApiService _performanceSettingService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public PerformanceController(
            IPerformanceApiService performanceSettingService,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            _performanceSettingService = performanceSettingService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
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
        public ActionResult<object> UpdatePerformanceSetting(object anySettig)
        {
            SettingsHelpers.AddOrUpdateAppSetting(anySettig, _webHostEnvironment);
            var results = new
            {
                test = _configuration["test"],
                test2 = _configuration["test2"],
                Message = _configuration["Message"]
            };
            return results;
        }

        //http://localhost:35200/Performance/GetPerformanceSetting
        [HttpGet]
        public ActionResult<object> GetPerformance()
        {
            return _performanceSettingService.GetPerformanceFromConsole();
        }

    }
}
