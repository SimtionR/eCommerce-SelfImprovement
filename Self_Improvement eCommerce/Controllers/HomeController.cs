using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Self_Improve_eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Controllers
{
    public class HomeController : ApiController
    {

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok("Works");
        }
        
        
    }
}
