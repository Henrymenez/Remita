using Remita.Cache.Interfaces;
using Remita.Models.Domains.Security;
using Remita.Services.Domains.Security.Dtos;

namespace Remita.Services.Domains.Security;

public class OtpCodeService : IOtpCodeService
{
    private readonly ICacheService _cacheService;
    private TimeSpan OtpValidity = TimeSpan.FromMinutes(5);
    public OtpCodeService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<string> GenerateOtpAsync(string userId, OtpOperation operation)
    {
        string cacheKey = CacheKeySelector.OtpCodeCacheKey(userId, operation);
        OtpCodeDto? otpCode = await _cacheService.ReadFromCache<OtpCodeDto>(cacheKey);
        if (otpCode != null)
        {
            otpCode.Otp = GenerateToken();
        }
        else
        {
            otpCode = new(GenerateToken());
        }
        await _cacheService.WriteToCache(cacheKey, otpCode, null, OtpValidity);
        return otpCode.Otp;
    }

    public async Task<bool> VerifyOtpAsync(string userId, string otp, OtpOperation operation)
    {
        string cacheKey = CacheKeySelector.OtpCodeCacheKey(userId, operation);
        OtpCodeDto? otpCode = await _cacheService.ReadFromCache<OtpCodeDto>(cacheKey);

        if (otpCode == null)
        {
            return false;
        }

        ++otpCode.Attempts;

        if (otpCode.Attempts >= 3)
        {
            await _cacheService.ClearFromCache(cacheKey);
            return false;
        }

        if (otpCode.Otp != otp)
        {
            return false;
        }
        return true;
    }

    private static string GenerateToken()
    {
        Random generator = new Random();
        string token = generator.Next(0, 999999).ToString("D6");

        return token;
    }
}
