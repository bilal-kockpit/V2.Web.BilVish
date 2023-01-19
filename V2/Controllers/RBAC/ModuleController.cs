using Microsoft.AspNetCore.Mvc;

namespace V2.Controllers.RBAC
{
    public class ModuleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
