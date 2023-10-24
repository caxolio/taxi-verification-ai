using TaxiVerificationIA.Models;

namespace TaxiVerificationIA.Services.Contract
{
    public interface ITaxiService
    {
        Task<List<Taxis>> GetTaxis();
        Task<Taxis> GetTaxiById(int id);
        Task<Taxis> GetTaxiByPlate(string plate);
        Task<Taxis> SaveTaxi(Taxis taximodel);
        Task<Taxis> UpdateTaxi(Taxis taximodel);
        Task<Boolean> DeleteTaxi(int id);
    }
}
