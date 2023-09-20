using Remita.Models.Domains.Security;
using Remita.Models.Enums;

namespace Remita.Services.Domains.Security;

public static class CacheKeySelector
{
    public static string OtpCodeCacheKey(string userId, OtpOperation operation)
    {
        return SHA256Hasher.Hash($"{CacheKeyPrefix.OtpCode}_{userId}_{operation}");
    }

    public static string AccountLockoutCacheKey(string userId)
    {
        return SHA256Hasher.Hash($"{CacheKeyPrefix.AccountLockout}_{userId}");
    }

}
