using Microsoft.AspNetCore.Mvc;
using Odyseusz.bll.Services.Interfaces;
using Odyseusz.domain;

namespace Odyseusz.Controllers
{
    public class ZgloszeniePodrozyController : Controller
    {
        private readonly IGenericService<ZgloszeniePodrozy, int> _service;

        public ZgloszeniePodrozyController(IGenericService<ZgloszeniePodrozy, int> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var zgloszenia = await _service.GetAllAsync();
            return View(zgloszenia); // Widok: Views/ZgloszeniePodrozy/Index.cshtml
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
            return View(); // Widok: Views/ZgloszeniePodrozy/Create.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZgloszeniePodrozy zgloszenie)
        {
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
