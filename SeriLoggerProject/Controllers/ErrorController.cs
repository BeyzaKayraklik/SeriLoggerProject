using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriLoggerProject.Controllers
{

    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var exceptionData = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.Message = "Invalid Operation";
            _logger.LogWarning("Exception url:{url}, exception message: {message}",
                       exceptionData?.Path, exceptionData?.Error.Message);
            return View();
        }
        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.Message = "Page Not Found!";
                    break;
                case 500:
                    ViewBag.Message = "Internal Server Error!";
                    break;
            }
            _logger.LogError("Status Code:{statusCode}, Request Path: {path}",
                                            statusCode, statusCodeData.OriginalPath);

            return View();
        }
    }
}
