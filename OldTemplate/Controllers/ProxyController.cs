using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OldTemplate.Controllers
{
    public class ProxyController : Controller
    {
        [HttpGet]
        public ActionResult<object> ProxyServiceTest()
        {
            var result = new
            {
                Data = "ProxyTest"
            };
            return result;
        }
    }
}
