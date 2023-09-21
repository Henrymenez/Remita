using Microsoft.AspNetCore.Mvc;
using Remita.Controllers.v1.Shared;
using Remita.Models.Utility;
using Remita.Services.Domains.Auth.Dtos;
using Remita.Services.Domains.Roles;
using Remita.Services.Domains.Roles.Dtos;
using Remita.Services.Utility;
using Swashbuckle.AspNetCore.Annotations;

namespace Remita.Api.Controllers.v1;

[Route("api/v{version:apiVersion}/claims")]
[ApiVersion("1.0")]
[ApiController]
public class RoleClaimController : BaseController
{
    private readonly IRoleClaimService _roleClaimsService;

    public RoleClaimController(IRoleClaimService roleClaimsService)
    {
        _roleClaimsService = roleClaimsService;
    }

    [HttpGet("get-claims", Name = "get-claims")]
    [SwaggerOperation(Summary = "returns claims of selected role")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<IEnumerable<ClaimDto>>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> GetClaims(string role)
    {
        var result = await _roleClaimsService.GetUserClaims(role);
        return Ok(result);
    }

    [HttpPost("add-claim", Name = "add-claim")]
    [SwaggerOperation(Summary = "adds claim to role")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<ClaimDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> AddClaim([FromBody] ClaimDto request)
    {
        var result = await _roleClaimsService.AddClaim(request);
        return ComputeApiResponse(result);
    }

    [HttpPost("delete-claim", Name = "delete-claim")]
    [SwaggerOperation(Summary = "deletes claims")]
    [ProducesResponseType(200, Type = typeof(ApiResponse))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> DeleteClaim(string claimValue, string role)
    {
       var result = await _roleClaimsService.RemoveUserClaims(claimValue, role);
        return ComputeResponse(result);
    }
}
