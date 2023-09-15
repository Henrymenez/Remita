using Remita.Services.Domains.Claims.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remita.Services.Domains.Claims;

public class RoleClaimService : IRoleClaimService
{
	public RoleClaimService()
	{

	}


    public Task<ClaimResponse> AddClaim(ClaimRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ClaimResponse>> GetUserClaims(string role)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveUserClaims(string claimType, string role)
    {
        throw new NotImplementedException();
    }
}
