using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProxyHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProxyHostController : Controller
    {
        [HttpGet]
        public object Get()
        {
            return new
            {
                Data = "ProxyHost"
            };
        }
    }
}
