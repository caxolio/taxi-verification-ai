using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using TaxiVerificationIA.Models;
using TaxiVerificationIA.Services.Contract;

namespace TaxiVerificationIA.Services.Implementation
{
    public class AgentService : IAgentService
    {
        private readonly TaxiVerificationAiContext _dbContext;
        public AgentService(TaxiVerificationAiContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<Agent>> GetAgents()
        {
            List<Agent> agentsList = await _dbContext.Agents
                                                     .Include(a => a.IdUserNavigation)
                                                     .ToListAsync();

            return agentsList;
        }

        public async Task<Agent> GetAgentById(int id)
        {
            Agent agent = await _dbContext.Agents
                .Where(t => t.IdAgent == id).SingleOrDefaultAsync();

            return agent;
        }

        public async Task<Agent> SaveAgent(Agent agentmodel)
        {
            _dbContext.Agents.Add(agentmodel);
            await _dbContext.SaveChangesAsync();
            return agentmodel;
        }

        public async Task<Agent> UpdateAgent(Agent agentmodel)
        {
            _dbContext.Agents.Update(agentmodel);
            await _dbContext.SaveChangesAsync();
            return agentmodel;
        }

        public async Task<bool> DeleteAgent(int id)
        {
            Agent agent = _dbContext.Agents.FirstOrDefault(t => t.IdAgent == id);

            if (agent != null)
            {
                _dbContext.Remove(agent);
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
