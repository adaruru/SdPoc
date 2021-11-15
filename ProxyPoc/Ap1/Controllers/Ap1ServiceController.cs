﻿using Ap1.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ap1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Ap1ServiceController : Controller
    {
        [HttpGet]
        public ResultPack Get()
        {
            var result = new ResultPack()
            {
                Status = "Success",
                Content = "Ap1Service"
            };
            return result;
        }
    }
}