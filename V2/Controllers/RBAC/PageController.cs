using Microsoft.AspNetCore.Mvc;

namespace V2.Controllers.RBAC
{
    public class PageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
