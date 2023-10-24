using Microsoft.EntityFrameworkCore;
using TaxiVerificationIA.Models;
using TaxiVerificationIA.Services.Contract;

namespace TaxiVerificationIA.Services.Implementation
{
    public class TaxiDriverService : ITaxiDriverService
    {
        private readonly TaxiVerificationAiContext _dbContext;
        public TaxiDriverService(TaxiVerificationAiContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TaxiDriver>> GetTaxiDrivers()
        {
            List<TaxiDriver> taxiDriverList = await _dbContext.TaxiDrivers
                                                              .Include(t => t.IdTaxiNavigation)
                                                              .Include(t => t.IdUserNavigation)
                                                              .ToListAsync();

            return taxiDriverList;
        }
        
        public async Task<TaxiDriver> GetTaxiDriverById(int id)
        {
            TaxiDriver taxidriver = await _dbContext.TaxiDrivers
                .Where(t => t.IdTaxiDriver == id).SingleOrDefaultAsync();

            return taxidriver;
        }

        public async Task<TaxiDriver> SaveTaxiDriver(TaxiDriver taxidrivermodel)
        {
            _dbContext.TaxiDrivers.Add(taxidrivermodel);
            await _dbContext.SaveChangesAsync();
            return taxidrivermodel;
        }

        public async Task<TaxiDriver> UpdateTaxiDriver(TaxiDriver taxidrivermodel)
        {
            _dbContext.TaxiDrivers.Update(taxidrivermodel);
            await _dbContext.SaveChangesAsync();
            return taxidrivermodel;
        }
        public async Task<bool> DeleteTaxiDriver(int id)
        {
            TaxiDriver taxidriver = _dbContext.TaxiDrivers.FirstOrDefault(t => t.IdTaxiDriver == id);

            if (taxidriver != null)
            {
                _dbContext.Remove(taxidriver);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
