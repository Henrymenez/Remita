using Microsoft.AspNetCore.Mvc;

namespace Remita.Api.Controllers.v1
{
    public class ApplicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
