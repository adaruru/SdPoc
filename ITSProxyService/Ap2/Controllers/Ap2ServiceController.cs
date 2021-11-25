using Ap2.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ap2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Ap2ServiceController : Controller
    {
        [HttpGet]
        public ActionResult<ResultPack> GetIndex()
        {
            var result = new ResultPack()
            {
                Status = "Success",
                Content = "Ap2Service"
            };
            return result;
        }
    }
}
