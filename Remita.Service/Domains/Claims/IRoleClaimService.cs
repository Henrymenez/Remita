using Remita.Services.Domains.Claims.Dtos;

namespace Remita.Services.Domains.Claims;

public interface IRoleClaimService
{
    Task<IEnumerable<ClaimResponse>> GetUserClaims(string role);
    Task<ClaimResponse> AddClaim(ClaimRequest request);
    Task<bool> RemoveUserClaims(string claimType, string role);
}
