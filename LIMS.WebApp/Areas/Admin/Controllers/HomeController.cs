using Microsoft.AspNetCore.Mvc;

namespace LIMS.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("lims/home")]
    [Route("lims")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
