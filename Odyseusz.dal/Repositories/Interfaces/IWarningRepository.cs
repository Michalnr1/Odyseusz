using Odyseusz.domain;

namespace Odyseusz.dal.Repositories.Interfaces
{
    public interface IWarningRepository
    {
        Task<IEnumerable<Ostrzezenie>> GetWarningsByCountryAsync(string countryName);
        Task AddWarningAsync(Ostrzezenie warning);
        Task SaveChangesAsync();
    }

}
