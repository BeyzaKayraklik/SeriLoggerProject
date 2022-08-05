using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeriLoggerProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SeriLoggerProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                if (id == 1)
                {
                    throw new Exception("Invalid Operation");
                }
                else if (id == 2)
                {
                    return StatusCode(500);
                }
                else if (id == 3)
                {
                    return NotFound();
                }
            }

            return View();
        }

      

       
        public IActionResult Privacy()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
