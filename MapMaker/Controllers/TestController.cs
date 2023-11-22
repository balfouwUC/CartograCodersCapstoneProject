using MapMaker.Models;
using Microsoft.AspNetCore.Mvc;

namespace MapMaker.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            MapMakerContext? context = HttpContext.RequestServices.GetService(typeof(MapMakerContext)) as MapMakerContext;

            return View(context.GetAllTests());
        }
    }
}
