using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;
using TaxiVerificationIA.Models;
using TaxiVerificationIA.Services.Contract;

namespace TaxiVerificationIA.Services.Implementation
{
    public class VerificationService : IVerificationService
    {
        private readonly TaxiVerificationAiContext _dbContext;
        private IWebHostEnvironment _webHostEnvironment;

        private static string _urlBaseApi = "";

        public VerificationService(TaxiVerificationAiContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;

            _webHostEnvironment = webHostEnvironment;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            _urlBaseApi = builder.GetSection("ConecctionApi.UrlBase").Value;
        }
        
        public async Task<List<Verification>> GetVerifications()
        {
            List<Verification> verificationsList = await _dbContext.Verifications
                                                                   .Include(v => v.IdTaxiNavigation)
                                                                   .Include(v => v.IdTaxiDriverNavigation)
                                                                   .Include(v => v.IdAgentNavigation)
                                                                   .Include(v => v.VerificationsImages)
                                                                   .Include(v => v.VerificationsResults)
                                                                   .ToListAsync();

            return verificationsList;
        }

        public async Task<Verification> GetVerificationsById(int id)
        {
            var verification = await _dbContext.Verifications
                                               .Include(v => v.VerificationsImages)
                                               .Include(v => v.IdTaxiNavigation)
                                               .Where(v => v.IdVerification == id)
                                               .SingleOrDefaultAsync();

            if (verification == null)
            {
                return new Verification();
            }

            return verification;
        }

        public Task<List<Verification>> GetVerificationsByAgent(int idAgent)
        {
            throw new NotImplementedException();
        }

        public async Task<Verification> SaveVerification(Verification verificationModel)
        {
            verificationModel.CreateDate = DateTime.Now;
            _dbContext.Verifications.Add(verificationModel);
            await _dbContext.SaveChangesAsync();
            return verificationModel;
        }

        public Task<Verification> UpdateVerification(Verification verificationModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteVerification(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<VerificationsImage> SaveVerificationImages(VerificationsImage verificationImagesModel)
        {
            verificationImagesModel.CreateDate = DateTime.Now;
            _dbContext.VerificationsImages.Add(verificationImagesModel);
            await _dbContext.SaveChangesAsync();
            return verificationImagesModel;
        }

        public async Task<VerificationsResult> ProcessVerification(int idVerification)
        {
            Verification verification = await GetVerificationsById(idVerification);

            var base64Frontal = "";
            var base64Lateral = "";

            if (verification.VerificationsImages.Count > 0) 
            {
                var verificationImages = verification.VerificationsImages.FirstOrDefault();

                var pathFrontal = GetActualPath(verificationImages.FrontImageName);
                var bytesFrontal = File.ReadAllBytes(pathFrontal);
                base64Frontal = Convert.ToBase64String(bytesFrontal);

                var pathLateral = GetActualPath(verificationImages.LeftImageName);
                var bytesLateral = File.ReadAllBytes(pathLateral);
                base64Lateral = Convert.ToBase64String(bytesLateral);
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri(_urlBaseApi);

            ApiRequest request = new ApiRequest();
            request.frontal = base64Frontal;
            request.lateral = base64Lateral;

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"verify", content);

            VerificationsResult verificationResult = new VerificationsResult();

            if (response.IsSuccessStatusCode) 
            {
                var jsonrespuesta = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ApiResult>(jsonrespuesta);

                if (result.verify_placa == verification.IdTaxiNavigation.Plate) 
                {
                    verificationResult.IsPlate = true;
                    verificationResult.PlateMatchAvg = 100;
                }

                if (result.verify_pegatina == verification.IdTaxiNavigation.Number.ToString())
                {
                    verificationResult.IsLabels = true;
                    verificationResult.LabelsMatchAvg = 100;
                }

                if (result.verify_pegatina == verification.IdTaxiNavigation.Number.ToString())
                {
                    verificationResult.IsColor = true;
                    verificationResult.ColorMatchAvg = 100;
                }

                if (verificationResult.IsPlate == true
                    && verificationResult.IsLabels == true
                    && verificationResult.IsColor == true) 
                {
                    verificationResult.IsApproved = true;
                }

                verificationResult.IdVerification = idVerification;

                verificationResult = await SaveVerificationResult(verificationResult);
            }

            return verificationResult;
        }

        public string GetActualPath(string FileName)
        {
            return Path.Combine(_webHostEnvironment.WebRootPath + "\\uploads\\taxis\\", FileName);
        }

        public async Task<VerificationsResult> SaveVerificationResult(VerificationsResult verificationResultModel)
        {
            verificationResultModel.VerificationDate = DateTime.Now;
            verificationResultModel.CreateDate = DateTime.Now;
            _dbContext.VerificationsResults.Add(verificationResultModel);
            await _dbContext.SaveChangesAsync();
            return verificationResultModel;
        }
    }
}
