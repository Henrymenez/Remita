using Microsoft.AspNetCore.Mvc;

namespace Remita.Api.Controllers.v1
{
    public class FileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
