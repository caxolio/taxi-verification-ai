using Microsoft.AspNetCore.Mvc;

using TaxiVerificationIA.Models;
using TaxiVerificationIA.Services.Contract;
using TaxiVerificationIA.Services.Implementation;

namespace TaxiVerificationIA.Controllers
{
    public class AgentsController : Controller
    {
        private readonly IAgentService _agentService;

        public AgentsController(AgentService agentService)
        {
            _agentService = agentService;
        }

        public async Task<IActionResult> Agents()
        {
            var agents = await _agentService.GetAgents();

            return View(agents);
        }
    }
}
