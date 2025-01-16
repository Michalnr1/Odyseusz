using Odyseusz.domain;
using System.Xml.Linq;
using Odyseusz.dal;
using Odyseusz.dal.Repositories.Interfaces;

namespace Odyseusz.bll.Services.Implementations
{
    public class WarningService
    {
        private readonly IWarningRepository _warningRepository;
        private readonly IGenericRepository<Kraj, string> _countryRepository;

        public WarningService(IWarningRepository warningRepository, IGenericRepository<Kraj, string> countryRepository)
        {
            _warningRepository = warningRepository;
            _countryRepository = countryRepository;
        }

        public async Task UpdateWarningsFromXml(string xmlPath)
        {
            var xmlDoc = XDocument.Load(xmlPath);

            var warnings = xmlDoc.Descendants("warning")
                .Select(x => new
                {
                    Country = x.Element("country")?.Value,
                    Message = x.Element("message")?.Value
                })
                .ToList();

            foreach (var warning in warnings)
            {
                if (string.IsNullOrWhiteSpace(warning.Country) || string.IsNullOrWhiteSpace(warning.Message))
                    continue;

                var existingWarnings = await _warningRepository.GetWarningsByCountryAsync(warning.Country);
                var latestWarning = existingWarnings.FirstOrDefault();

                if (latestWarning == null || latestWarning.Tresc != warning.Message)
                {
                    var country = await _countryRepository.GetByIdAsync(warning.Country);

                    if (country != null)
                    {
                        var newWarning = new Ostrzezenie
                        {
                            KrajNazwa = warning.Country,
                            Kraj = country,
                            Tresc = warning.Message,
                            Data = DateTime.Now
                        };

                        await _warningRepository.AddWarningAsync(newWarning);
                    }
                }
            }

            await _warningRepository.SaveChangesAsync();
        }
    }
}
