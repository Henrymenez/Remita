using Remita.Services.Domains.Roles.Dtos;
using Remita.Services.Utility;

namespace Remita.Services.Domains.Roles;

public interface IRoleClaimService
{
    Task<ServiceResponse<IEnumerable<ClaimDto>>> GetUserClaims(string role);
    Task<ServiceResponse<ClaimDto>> AddClaim(ClaimDto request);
    Task<ServiceResponse> RemoveUserClaims(string claimType, string role);
}
