using TaxiVerificationIA.Models;

namespace TaxiVerificationIA.Services.Contract
{
    public interface IVerificationService
    {
        Task<List<Verification>> GetVerifications();
        Task<Verification> GetVerificationsById(int id);
        Task<List<Verification>> GetVerificationsByAgent(int idAgent);
        Task<Verification> SaveVerification(Verification verificatioMmodel);
        Task<Verification> UpdateVerification(Verification verificationModel);
        Task<Boolean> DeleteVerification(int id);
        Task<VerificationsImage> SaveVerificationImages(VerificationsImage verificationImageModel);
        Task<VerificationsResult> ProcessVerification(int idVerification);
    }
}
