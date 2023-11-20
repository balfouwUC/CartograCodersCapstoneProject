using MapMaker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MapMaker.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            MapIndexViewModel mapIdxVM = new MapIndexViewModel();

            return View(mapIdxVM);
        }
    }
}
