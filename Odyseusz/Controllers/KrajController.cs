using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Odyseusz.bll.Services.Interfaces;
using Odyseusz.domain;

namespace Odyseusz.Controllers
{
    //[Route("Database/[controller]")]
    public class KrajController : Controller
    {
        private readonly IGenericService<Kraj, string> _service;

        public KrajController(IGenericService<Kraj, string> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var kraje = await _service.GetAllAsync();
            return View("~/Views/Database/Kraj/Index.cshtml", kraje); // Widok: Views/Database/Kraj/Index.cshtml
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var kraj = await _service.GetByIdAsync(id);
            if (kraj == null)
                return NotFound();

            return View(kraj); // Widok: Views/Database/Kraj/Details.cshtml
        }
        /*
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(); // Widok: Views/Kraj/Create.cshtml
        }*/

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var kraje = await _service.GetAllAsync();

            TempData["TempCountries"] = JsonSerializer.Serialize(kraje);

            return RedirectToAction("Create", "ZgloszeniePodrozy");
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kraj kraj)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(kraj);
                return RedirectToAction(nameof(Index));
            }
            return View(kraj);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var kraj = await _service.GetByIdAsync(id);
            if (kraj == null)
                return NotFound();

            return View(kraj); // Widok: Views/Kraj/Edit.cshtml
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Kraj kraj)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(kraj);
                return RedirectToAction(nameof(Index));
            }
            return View(kraj);
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var kraj = await _service.GetByIdAsync(id);
            if (kraj == null)
                return NotFound();

            return View(kraj); // Widok: Views/Kraj/Delete.cshtml
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
