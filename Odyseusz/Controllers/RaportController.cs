using Microsoft.AspNetCore.Mvc;
using Odyseusz.Models;
using Odyseusz.domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Text.Json;

namespace Odyseusz.Controllers
{
    public class RaportController : Controller
    {
        private static List<Kraj> kraje;

        public IActionResult Create()
        {
            var krajeJson = TempData["TempCountries"] as string;

            if (string.IsNullOrEmpty(krajeJson))
            {
                return RedirectToAction("getCountriesForReport", "Kraj");
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

            return View("~/Views/Raport/Create.cshtml");
        }

        // Akcja do generowania raportu
        [HttpPost]
        public IActionResult GenerateReport(ReportRequestViewModel request)
        {
            if (ModelState.IsValid)
            {
                // Generujemy dane raportu
                var reportEntries = GenerateReportData(request.StartDate, request.EndDate, request.Countries);

                // Generujemy listę miesięcy w zadanym przedziale
                var monthsInRange = GetMonthsInRange(request.StartDate, request.EndDate);

                // Przygotowanie modelu raportu
                var model = new ReportViewModel
                {
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    GeneratedDate = DateTime.Now,
                    ReportEntries = reportEntries,
                    MonthsInRange = monthsInRange
                };

                // Zwracamy widok z raportem
                return View("~/Views/Raport/Index.cshtml", model);
            }

            return View("Create", request);
        }

        // Metoda pomocnicza do generowania danych raportu
        private List<ReportEntry> GenerateReportData(DateTime startDate, DateTime endDate, List<string> countries)
        {
            var reportEntries = new List<ReportEntry>();

            // Symulacja danych raportu (w prawdziwej aplikacji dane będą pochodziły np. z bazy danych)
            foreach (var country in countries)
            {
                var monthlyData = new List<MonthlyData>();

                // Generowanie danych dla każdego miesiąca w przedziale
                var currentDate = new DateTime(startDate.Year, startDate.Month, 1);
                while (currentDate <= endDate)
                {
                    monthlyData.Add(new MonthlyData
                    {
                        Month = currentDate,
                        TripCount = new Random().Next(1, 10) // Losowa liczba podróży w danym miesiącu
                    });

                    // Przechodzimy do kolejnego miesiąca
                    currentDate = currentDate.AddMonths(1);
                }

                // Tworzymy wpis raportu dla danego kraju
                reportEntries.Add(new ReportEntry
                {
                    Id = reportEntries.Count + 1,
                    NazwaKraju = country,
                    MonthlyData = monthlyData,
                    TotalTrips = monthlyData.Sum(m => m.TripCount) // Liczba podróży łącznie
                });
            }

            return reportEntries;
        }

        // Metoda pomocnicza do generowania listy miesięcy w zadanym przedziale dat
        private List<DateTime> GetMonthsInRange(DateTime startDate, DateTime endDate)
        {
            var months = new List<DateTime>();

            var currentDate = new DateTime(startDate.Year, startDate.Month, 1);
            while (currentDate <= endDate)
            {
                months.Add(currentDate); // Dodajemy pierwszy dzień miesiąca
                currentDate = currentDate.AddMonths(1);
            }

            return months;
        }
    }
}
