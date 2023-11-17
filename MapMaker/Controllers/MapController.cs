using Microsoft.AspNetCore.Mvc;

namespace MapMaker.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
