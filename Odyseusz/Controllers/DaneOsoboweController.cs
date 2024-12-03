using Microsoft.AspNetCore.Mvc;
using Odyseusz.bll.Services.Interfaces;
using Odyseusz.domain;

namespace Odyseusz.Controllers
{
    public class DaneOsoboweController : Controller
    {
        private readonly IGenericService<DaneOsobowe, int> _service;

        public DaneOsoboweController(IGenericService<DaneOsobowe, int> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var daneOsoboweList = await _service.GetAllAsync();
            return View(daneOsoboweList); // Widok: Views/DaneOsobowe/Index.cshtml
        }

        public async Task<IActionResult> Details(int id)
        {
            var daneOsobowe = await _service.GetByIdAsync(id);
            if (daneOsobowe == null)
                return NotFound();

            return View(daneOsobowe); // Widok: Views/DaneOsobowe/Details.cshtml
        }

        public IActionResult Create()
        {
            return View(); // Widok: Views/DaneOsobowe/Create.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DaneOsobowe daneOsobowe)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(daneOsobowe);
                return RedirectToAction(nameof(Index));
            }
            return View(daneOsobowe);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var daneOsobowe = await _service.GetByIdAsync(id);
            if (daneOsobowe == null)
                return NotFound();

            return View(daneOsobowe); // Widok: Views/DaneOsobowe/Edit.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DaneOsobowe daneOsobowe)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(daneOsobowe);
                return RedirectToAction(nameof(Index));
            }
            return View(daneOsobowe);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var daneOsobowe = await _service.GetByIdAsync(id);
            if (daneOsobowe == null)
                return NotFound();

            return View(daneOsobowe); // Widok: Views/DaneOsobowe/Delete.cshtml
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
