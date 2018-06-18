using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Controllers
{
    [Route("[controller]")]
    public class AboutController : Controller
    {
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
