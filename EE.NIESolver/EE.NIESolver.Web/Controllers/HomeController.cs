using Microsoft.AspNetCore.Mvc;

namespace EE.NIESolver.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
