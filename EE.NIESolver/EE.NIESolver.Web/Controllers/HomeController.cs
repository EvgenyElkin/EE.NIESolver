using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Constants.Account;
using Microsoft.AspNetCore.Mvc;

namespace EE.NIESolver.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConstantService _service;

        public HomeController(IConstantService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Json(new
            {
                IsSuccess = true,
                EnumToInt = _service.GetId(_service.GetEnum<Roles>(1)),
                IntToEnum = _service.GetId(_service.GetEnum<Roles>(2))
            });
        }
    }
}
