using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LotteryResult.Controllers
{
    public class ErrorController : Controller
    {
        // GET Error/NotFound
        [HttpGet]
        public ActionResult NotFound()
        {
            var statusCode = (int)System.Net.HttpStatusCode.NotFound;
            Response.StatusCode = statusCode;
            Response.TrySkipIisCustomErrors = true;
            HttpContext.Response.StatusCode = statusCode;
            HttpContext.Response.TrySkipIisCustomErrors = true;
            return View("~/Views/Shared/Error_404.cshtml");
        }

        // GET Error/InternalServerError
        [HttpGet]
        public ActionResult InternalServerError()
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            Response.TrySkipIisCustomErrors = true;
            return View("~/Views/Shared/Error_500.cshtml");
        }
    }
}