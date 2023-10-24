using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaxiVerificationIA.Models;
using TaxiVerificationIA.Services.Contract;
using TaxiVerificationIA.Services.Implementation;
using System.IO;

namespace TaxiVerificationIA.Controllers
{
    [RequestSizeLimit(52428800)]
    public class VerificationsController : Controller
    {
        private readonly IVerificationService _verificationService;
        private IWebHostEnvironment _webHostEnvironment;

        public VerificationsController(VerificationService verificationService, IWebHostEnvironment webHostEnvironment)
        {
            _verificationService = verificationService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ActionResult> Verifications()
        {
            var verifications = await _verificationService.GetVerifications();

            return View(verifications);
        }

        public async Task<ActionResult> AddVerification()
        {
            Verification verificationmodel = new();
            verificationmodel.Folio = "VF" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString()
                                            + DateTime.Today.Day.ToString() + DateTime.Now.Hour.ToString()
                                            + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString("00");

            ClaimsPrincipal claimuser = HttpContext.User;
            int idAgent = 0;

            if(claimuser.Identity.IsAuthenticated)
            {
                string sIdAgent = claimuser.Claims.Where(c => c.Type == ClaimTypes.PrimarySid)
                            .Select(c => c.Value).SingleOrDefault();

                verificationmodel.IdAgent = Convert.ToInt32(sIdAgent);
            }

            return View(verificationmodel);
        }

        [HttpPost]
        public async Task<ActionResult> AddVerification(Verification verificationModel)
        {
            if (verificationModel.IdTaxi == null
                || verificationModel.IdTaxiDriver == null
                || verificationModel.IdAgent == null)
            {
                ViewData["Message"] = "Debe capturar un número de placa.";
                return View(verificationModel);
            }

            Verification verification = await _verificationService.SaveVerification(verificationModel);

            if(verification.IdVerification > 0)
                return RedirectToAction("UploadImages", "Verifications", verification);

            ViewData["Message"] = "No se pudo crear la Verificación";
            return View(verificationModel);
        }

        public async Task<ActionResult> UploadImages(Verification verificationModel) 
        { 

            VerificationsImage verificationsImageModel = new VerificationsImage();
            verificationsImageModel.IdVerification = verificationModel.IdVerification;
            verificationsImageModel.IdVerificationNavigation = new Verification();
            verificationsImageModel.IdVerificationNavigation.Folio = verificationModel.Folio;
            verificationsImageModel.AcceptCompliance = true;

            return View(verificationsImageModel);
        }

        [HttpPost]
        public async Task<ActionResult> UploadImages(VerificationsImage verificationImageModel)
        {
            if (verificationImageModel.FrontImageName == null
                || verificationImageModel.LeftImageName == null
                || verificationImageModel.RightImageName == null)
            {
                ViewData["Message"] = "Debe cargar todas las imagenes del vehiculo.";
                return View(verificationImageModel);
            }

            if (verificationImageModel.AcceptCompliance.Equals(false))
            {
                ViewData["Message"] = "Debe aceptar el acuerdo de conformidad.";
                return View(verificationImageModel);
            }

            VerificationsImage verificationImages = await _verificationService.SaveVerificationImages(verificationImageModel);

            if (verificationImages.IdVerificationImages > 0) 
            {
                var id = verificationImages.IdVerification == null ? 0 : verificationImages.IdVerification.Value;

                var verificationResult = await _verificationService.ProcessVerification(id);

                if (verificationResult.IdVerificationResult > 0) 
                { 
                    return RedirectToAction("VerificationResult", "Verifications", verificationResult);
                }
            }
                
            return View(verificationImageModel);
        }

        [HttpPost]
        public async Task<ActionResult> UploadImage() 
        { 
            string Result = string.Empty;
            var Files = Request.Form.Files;
            foreach (IFormFile source in Files)
            { 
                string filename = source.Name + ".jpeg";
                string imagepath = GetActualPath(filename);

                try 
                { 
                    if(System.IO.File.Exists(imagepath))
                        System.IO.File.Delete(imagepath);

                    using (FileStream stream = System.IO.File.Create(imagepath)) 
                    { 
                        await source.CopyToAsync(stream);
                        Result = "pass";
                    }
                }
                catch (Exception ex) 
                {
                    throw ex;
                }
            }

            return Ok(Result);
        }

        public string GetActualPath(string FileName) 
        {
            return Path.Combine(_webHostEnvironment.WebRootPath + "\\uploads\\taxis\\", FileName);
        }

        public async Task<ActionResult> VerificationResult(VerificationsResult verificationsResultModel) 
        {
            return View(verificationsResultModel);
        }
    }
}
