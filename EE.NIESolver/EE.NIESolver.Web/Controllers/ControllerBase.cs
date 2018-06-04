using Microsoft.AspNetCore.Mvc;

namespace EE.NIESolver.Web.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected JsonResult JsonResult(bool result, object item = null)
        {
            return Json(new
            {
                IsSuccess = result,
                Item = item
            });
        }
    }
}