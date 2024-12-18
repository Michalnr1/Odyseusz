using Microsoft.AspNetCore.Mvc;
using Odyseusz.bll.Services.Interfaces;
using Odyseusz.domain;

namespace Odyseusz.Controllers
{
    public class AdresController : Controller
    {
        private readonly IGenericService<Adres, int> _service;

        public AdresController(IGenericService<Adres, int> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var adresy = await _service.GetAllAsync();
            return View("~/Views/Database/Adres/Index.cshtml", adresy); // Widok: Views/Database/Adres/Index.cshtml
        }

        public async Task<IActionResult> Details(int id)
        {
            var adres = await _service.GetByIdAsync(id);
            if (adres == null)
                return NotFound();

            return View(adres); // Widok: Views/Adres/Details.cshtml
        }

        public IActionResult Create()
        {
            return View(); // Widok: Views/Adres/Create.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Adres adres)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(adres);
                return RedirectToAction(nameof(Index));
            }
            return View(adres);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var adres = await _service.GetByIdAsync(id);
            if (adres == null)
                return NotFound();

            return View(adres); // Widok: Views/Adres/Edit.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Adres adres)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(adres);
                return RedirectToAction(nameof(Index));
            }
            return View(adres);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var adres = await _service.GetByIdAsync(id);
            if (adres == null)
                return NotFound();

            return View(adres); // Widok: Views/Adres/Delete.cshtml
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
