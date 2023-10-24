using TaxiVerificationIA.Models;

namespace TaxiVerificationIA.Services.Contract
{
    public interface ITaxiDriverService
    {
        Task<List<TaxiDriver>> GetTaxiDrivers();
        Task<TaxiDriver> GetTaxiDriverById(int id);

        Task<TaxiDriver> SaveTaxiDriver(TaxiDriver taxidrivermodel);

        Task<TaxiDriver> UpdateTaxiDriver(TaxiDriver taxidrivermodel);

        Task<Boolean> DeleteTaxiDriver(int id);
    }
}
