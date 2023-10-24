using Microsoft.AspNetCore.Mvc;

using TaxiVerificationIA.Models;
using TaxiVerificationIA.Services.Contract;
using TaxiVerificationIA.Services.Implementation;

namespace TaxiVerificationIA.Controllers
{
    public class TaxisController : Controller
    {
        private readonly ITaxiService _taxiService;

        public TaxisController(TaxiService taxiService)
        {
            _taxiService = taxiService;
        }

        // GET: TaxisController
        public async Task<ActionResult> Taxis()
        {
            var taxis = await _taxiService.GetTaxis();

            return View(taxis);
        }

        [HttpPost]
        public async Task<JsonResult>  GetTaxiByPlate(string plate) 
        {
            var taxi = await _taxiService.GetTaxiByPlate(plate);

            return Json(taxi);
        }

        [HttpPost]
        public ActionResult AddTaxis(Taxis taxiModel)
        {
            return View();
        }
    }
}
