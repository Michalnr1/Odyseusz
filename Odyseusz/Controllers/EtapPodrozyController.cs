using Microsoft.AspNetCore.Mvc;
using Odyseusz.bll.Services.Interfaces;
using Odyseusz.domain;

namespace Odyseusz.Controllers
{
    public class EtapPodrozyController : Controller
    {
        private readonly IGenericService<EtapPodrozy, int> _service;

        public EtapPodrozyController(IGenericService<EtapPodrozy, int> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            //var etapyPodrozy = await _service.GetAllAsync();
            var etapyPodrozy = await _service.GetAllIncludingAsync(
                e => e.Adres
            );
            return View("~/Views/Database/EtapPodrozy/Index.cshtml", etapyPodrozy); // Widok: Views/Database/EtapPodrozy/Index.cshtml
        }

        public async Task<IActionResult> Details(int id)
        {
            var etapPodrozy = await _service.GetByIdAsync(id);
            if (etapPodrozy == null)
                return NotFound();

            return View(etapPodrozy); // Widok: Views/EtapPodrozy/Details.cshtml
        }

        public IActionResult Create()
        {
            return View(); // Widok: Views/EtapPodrozy/Create.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EtapPodrozy etapPodrozy)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(etapPodrozy);
                return RedirectToAction(nameof(Index));
            }
            return View(etapPodrozy);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var etapPodrozy = await _service.GetByIdAsync(id);
            if (etapPodrozy == null)
                return NotFound();

            return View(etapPodrozy); // Widok: Views/EtapPodrozy/Edit.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EtapPodrozy etapPodrozy)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(etapPodrozy);
                return RedirectToAction(nameof(Index));
            }
            return View(etapPodrozy);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var etapPodrozy = await _service.GetByIdAsync(id);
            if (etapPodrozy == null)
                return NotFound();

            return View(etapPodrozy); // Widok: Views/EtapPodrozy/Delete.cshtml
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
