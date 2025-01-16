using Odyseusz.dal.Repositories.Interfaces;
using Odyseusz.domain;
using Microsoft.EntityFrameworkCore;

namespace Odyseusz.dal.Repositories.Implementations
{
    public class WarningRepository : IWarningRepository
    {
        private readonly OdyseuszDbContext _context;

        public WarningRepository(OdyseuszDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ostrzezenie>> GetWarningsByCountryAsync(string countryName)
        {
            return await _context.Ostrzezenia
                .Where(w => w.KrajNazwa == countryName)
                .OrderByDescending(w => w.Data)
                .ToListAsync();
        }

        public async Task AddWarningAsync(Ostrzezenie warning)
        {
            await _context.Ostrzezenia.AddAsync(warning);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
