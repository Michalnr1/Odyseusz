using System.Text.Json;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Odyseusz.bll.Services.Interfaces;
using Odyseusz.domain;

namespace Odyseusz.Controllers
{
    public class ZgloszeniePodrozyController : Controller
    {
        private readonly IGenericService<ZgloszeniePodrozy, int> _service;
        private static List<Kraj> kraje;

        public ZgloszeniePodrozyController(IGenericService<ZgloszeniePodrozy, int> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            //var zgloszenia = await _service.GetAllAsync();
            var zgloszenia = await _service.GetAllIncludingAsync(
                z => z.DaneOsobowe,
                z => z.DaneOsobowe.Adres,
                z => z.DaneOsobowe.Adres.Kraj,
                z => z.EtapyPodrozy
                //z => z.EtapyPodrozy.Select(e => e.Adres),
                //z => z.EtapyPodrozy.Select(e => e.Adres.Kraj)
            );
            return View("~/Views/Database/ZgloszeniePodrozy/Index.cshtml", zgloszenia); // Widok: Views/Database/ZgloszeniePodrozy/Index.cshtml
        }

        public async Task<IActionResult> Details(int id)
        {
            var zgloszenie = await _service.GetByIdAsync(id);
            if (zgloszenie == null)
                return NotFound();

            return View(zgloszenie); // Widok: Views/ZgloszeniePodrozy/Details.cshtml
        }

        public IActionResult Create()
        {
            ViewData["OrganizatorPobytuList"] = Enum.GetValues(typeof(OrganizatorPobytu))
            .Cast<OrganizatorPobytu>()
            .Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = e.ToString()
            })
            .ToList();
            
            var krajeJson = TempData["TempCountries"] as string;

            if (string.IsNullOrEmpty(krajeJson))
            {
                return RedirectToAction("Create", "Kraj");
            }

            kraje = JsonSerializer.Deserialize<List<Kraj>>(krajeJson);
            
            //var kraje = await _serviceKraj.GetAllAsync();

            ViewData["Countries"] = kraje.Select(k => new SelectListItem
            {
                Value = k.Nazwa,
                Text = k.Nazwa  
            }).ToList();

            // Serializacja do JSON i przekazanie jako ViewData
            ViewData["CountriesData"] = JsonSerializer.Serialize(
                kraje.Select(k => new { k.KodKraju, k.Nazwa })
            );


            return View(); // Widok: Views/ZgloszeniePodrozy/Create.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZgloszeniePodrozy zgloszenie)
        {
            var kraj = kraje.FirstOrDefault(k => k.Nazwa == zgloszenie.DaneOsobowe.Adres.Kraj.Nazwa);
            zgloszenie.DaneOsobowe.Adres.Kraj = kraj;

            // Upewnij się, że każdy etap podróży korzysta z istniejącego kraju
            foreach (var etap in zgloszenie.EtapyPodrozy)
            {
                kraj = kraje.FirstOrDefault(k => k.Nazwa == etap.Adres.Kraj.Nazwa);

                if (kraj != null)
                {
                    etap.Adres.Kraj = kraj; // Przypisz istniejący rekord
                }
                else
                {
                    // Obsługa przypadku, gdy kraj nie został znaleziony (opcjonalnie)
                    ModelState.AddModelError("", $"Kraj {etap.Adres.Kraj.Nazwa} nie istnieje w bazie.");
                    return View(zgloszenie);
                }
            }

            if (ModelState.IsValid)
            {
                await _service.AddAsync(zgloszenie);
                return RedirectToAction(nameof(Index));
            }
            return View(zgloszenie);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var zgloszenie = await _service.GetByIdAsync(id);
            if (zgloszenie == null)
                return NotFound();

            return View(zgloszenie); // Widok: Views/ZgloszeniePodrozy/Edit.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ZgloszeniePodrozy zgloszenie)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(zgloszenie);
                return RedirectToAction(nameof(Index));
            }
            return View(zgloszenie);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var zgloszenie = await _service.GetByIdAsync(id);
            if (zgloszenie == null)
                return NotFound();

            return View(zgloszenie); // Widok: Views/ZgloszeniePodrozy/Delete.cshtml
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
