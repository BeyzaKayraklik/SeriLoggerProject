using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriLoggerProject.Controllers
{
    public class DivideByZeroController : Controller
    {
        public IActionResult DivideByZeroC()
        {
            var a = 0;
            int b = 10 / a;
        
            return View();  
        }
      
    }
}
