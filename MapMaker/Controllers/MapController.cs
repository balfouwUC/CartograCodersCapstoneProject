using MapMaker.Data;
using MapMaker.Data.Services;
using MapMaker.Models;
using MapMaker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MapMaker.Controllers
{
    public class MapController : Controller
    {
        private readonly IMapsService _mapsService;
        
        public MapController(IMapsService service)
        {
            _mapsService = service;
        }
        
        public async Task<IActionResult> Index(string canvasColor, string landColor, string textColor)
        {
            var data = await _mapsService.GetAllAsync();

            MapIndexViewModel mapIdxVM;
            if (!string.IsNullOrEmpty(canvasColor) && !string.IsNullOrEmpty(landColor) && !string.IsNullOrEmpty(textColor))
                mapIdxVM = new MapIndexViewModel
                {
                    ColorPalette = new ColorPaletteViewModel(canvasColor, landColor, textColor),
                    Maps = data
                };
            else
            {
                mapIdxVM = new MapIndexViewModel();
            }

            return View(mapIdxVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(string url)
        {
            if (ModelState.IsValid)
            {
                return View();
            }

            var newMap = new Map();
            newMap.Data = url;
            await _mapsService.AddAsync(newMap);
            return RedirectToAction(nameof(Index));
        }
    }
}
