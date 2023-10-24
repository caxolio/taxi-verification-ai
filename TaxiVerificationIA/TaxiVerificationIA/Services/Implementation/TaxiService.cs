using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using TaxiVerificationIA.Models;
using TaxiVerificationIA.Services.Contract;

namespace TaxiVerificationIA.Services.Implementation
{
    public class TaxiService : ITaxiService
    {
        private readonly TaxiVerificationAiContext _dbContext;

        public TaxiService(TaxiVerificationAiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Taxis>> GetTaxis() 
        {
            List<Taxis> taxisList = await _dbContext.Taxes
                                                    .Include(t => t.IdBrandNavigation)
                                                    .Include(t => t.IdModelNavigation)
                                                    .Include(t => t.IdColorNavigation)
                                                    .ToListAsync();

            return taxisList;
        }
        public async Task<Taxis> GetTaxiById(int id)
        {
            Taxis taxi = await _dbContext.Taxes
                .Where(t => t.IdTaxi == id).SingleOrDefaultAsync();

            return taxi;
        }

        public async Task<Taxis> GetTaxiByPlate(string plate)
        {
            Taxis taxi = await _dbContext.Taxes
                .Where(t => t.Plate == plate)
                .Include(t => t.TaxiDrivers)
                .Include(t => t.IdBrandNavigation)
                .Include(t => t.IdModelNavigation)
                .Include(t => t.IdColorNavigation)
                .SingleOrDefaultAsync();

            return taxi;
        }

        public async Task<Taxis> SaveTaxi(Taxis taximodel)
        {
            _dbContext.Taxes.Add(taximodel);
            await _dbContext.SaveChangesAsync();
            return taximodel;
        }

        public async Task<Taxis> UpdateTaxi(Taxis taximodel) 
        { 
            _dbContext.Taxes.Update(taximodel);
            await _dbContext.SaveChangesAsync();
            return taximodel;
        }

        public async Task<Boolean> DeleteTaxi(int id) 
        { 
            Taxis taxi = _dbContext.Taxes.FirstOrDefault(t => t.IdTaxi==id);

            if (taxi != null)
            {
                _dbContext.Remove(taxi);
                await _dbContext.SaveChangesAsync();
            }
            else { 
                return false;
            }

            return true;
        }
    }
}
