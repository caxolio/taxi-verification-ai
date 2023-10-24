using Microsoft.AspNetCore.Mvc;

using TaxiVerificationIA.Models;
using TaxiVerificationIA.Services.Contract;
using TaxiVerificationIA.Services.Implementation;

namespace TaxiVerificationIA.Controllers
{
    public class TaxiDriversController : Controller
    {
        private readonly ITaxiDriverService _taxiDriverService;

        public TaxiDriversController(TaxiDriverService taxiDriverService)
        {
            _taxiDriverService = taxiDriverService;
        }

        public async Task<IActionResult> TaxiDrivers()
        {
            var taxiDrivers = await _taxiDriverService.GetTaxiDrivers();

            return View(taxiDrivers);
        }
    }
}
