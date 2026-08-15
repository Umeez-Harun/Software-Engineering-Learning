using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_2.Controllers
{
    public class HomeController : Controller
    {
        [Route("/Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            IExceptionHandlerPathFeature? ex = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (ex != null && ex.Error.InnerException != null)
            {
                ViewBag.ErrorMessage = ex.Error.InnerException.Message;
            }
            if(ex != null && ex.Error.Message != null)
            {
                ViewBag.ErrorMessage = ex.Error.Message;
            }
            return View("Error");
        }
    }
}
