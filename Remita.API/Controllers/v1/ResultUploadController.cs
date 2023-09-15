using Microsoft.AspNetCore.Mvc;
using Remita.Controllers.v1.Shared;
using Remita.Services.Domains.Results;
using Remita.Services.Domains.Results.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Remita.Api.Controllers.v1;

public class ResultUploadController : BaseController
{
    private readonly IResultService _resultDecision;
    public ResultUploadController(IResultService resultDecision)
    {
        _resultDecision = resultDecision;
    }
    [HttpPut("ApproveResult")]
    [SwaggerOperation(Summary = "Reviews Pending Results")]
  /*  [SwaggerResponse(StatusCodes.Status200OK, Description = "The Result of a Student", Type = typeof(ResultResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid ResultId", Type = typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid ApprovalId", Type = typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to Send Mail", Type = typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]*/

    public async Task<IActionResult> Review(ResultRequest request)
    {
        var response = await _resultDecision.ApproveResult(request);
        return Ok(response);

    }

  /*  [HttpGet("Uploaded-Pending-Results")]
    [SwaggerOperation(Summary = "Get All Uploaded Pending Results")]*/
    //[SwaggerResponse(StatusCodes.Status200OK, Description = "View uploaded pending results", Type = typeof(ResultResponse))]
   /* public async Task<IActionResult> Get([FromQuery] Pagination paging, [FromQuery] UploadedResultRequest resultRequest)
    {
        var results = await _uploadedResultServices.Filter(paging, resultRequest);

        return Ok(results);
    }*/
}
