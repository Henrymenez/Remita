using Remita.Services.Domains.Roles.Dtos;
using Remita.Services.Utility;

namespace Remita.Services.Domains.Roles;

public class RoleClaimService : IRoleClaimService
{
    public RoleClaimService()
    {

    }
    public Task<ServiceResponse<ClaimDto>> AddClaim(ClaimDto request)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ClaimDto>> GetUserClaims(string role)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> RemoveUserClaims(string claimType, string role)
    {
        throw new NotImplementedException();
    }
}
