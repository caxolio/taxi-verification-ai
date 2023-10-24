using Microsoft.EntityFrameworkCore;
using TaxiVerificationIA.Models;

namespace TaxiVerificationIA.Services.Contract
{
    public interface IUserService
    {
        Task<User> GetUser(string email, string pass);

        Task<User> SaveUser(User usermodel);
    }
}
