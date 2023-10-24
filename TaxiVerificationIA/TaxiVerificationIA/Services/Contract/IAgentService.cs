using TaxiVerificationIA.Models;

namespace TaxiVerificationIA.Services.Contract
{
    public interface IAgentService
    {
        Task<List<Agent>> GetAgents();
        Task<Agent> GetAgentById(int id);

        Task<Agent> SaveAgent(Agent agentmodel);

        Task<Agent> UpdateAgent(Agent agentmodel);

        Task<Boolean> DeleteAgent(int id);
    }
}
