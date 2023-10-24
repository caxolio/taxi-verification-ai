using Microsoft.EntityFrameworkCore;
using TaxiVerificationIA.Models;
using TaxiVerificationIA.Services.Contract;

namespace TaxiVerificationIA.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly TaxiVerificationAiContext _dbContext;

        public UserService(TaxiVerificationAiContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<User> GetUser(string email, string pass)
        {
            User user = await _dbContext.Users
                .Include(u => u.Agents)
                .Where(u=>u.Email == email && u.Password == pass)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> SaveUser(User usermodel)
        {
            _dbContext.Users.Add(usermodel);
            await _dbContext.SaveChangesAsync();
            return usermodel;
        }
    }
}
