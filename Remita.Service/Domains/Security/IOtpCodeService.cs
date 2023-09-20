using Remita.Models.Domains.Security;

namespace Remita.Services.Domains.Security;

public interface IOtpCodeService
{
    Task<string> GenerateOtpAsync(string userId, OtpOperation operation);
    Task<bool> VerifyOtpAsync(string userId, string otp, OtpOperation operation);
}
