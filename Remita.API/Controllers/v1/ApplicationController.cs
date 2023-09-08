using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Remita.Controllers.v1.Shared;
using Remita.Services.Domains.Transcript;
using Remita.Services.Domains.Transcript.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Remita.Api.Controllers.v1;

[Route("api/v{version:apiVersion}/transcriptapplication")]
[ApiVersion("1.0")]
[ApiController]
public class ApplicationController : BaseController
{
    private readonly ITranscriptService _transcriptApplicationService;
    //  private TranscriptAppParameter transcriptAppParameter = new TranscriptAppParameter();
    public ApplicationController(ITranscriptService transcriptApplicationService)
    {
        _transcriptApplicationService = transcriptApplicationService;
    }

    [HttpPut("update-transcript-status/{Id}", Name = "Update transcript status")]
    [SwaggerOperation(Summary = "Updates the application transcript status")]
    /*  [SwaggerResponse(StatusCodes.Status200OK, Description = "Status updated successfully", Type = typeof(ExportResponse))]
      [SwaggerResponse(StatusCodes.Status204NoContent, Description = "Update Unsuccessful", Type = typeof(ExportResponse))]
      [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ExportResponse))]*/
    public async Task<IActionResult> UpdateTranscriptStatus([FromRoute] string Id, TranscriptRequestDto request)
    {
        var response = await _transcriptApplicationService.UpdateApplicationStatus(request, Id);
        return Ok(response);
    }


    /// <summary>
    /// This endpoint takes in the Create Application Request 
    /// It creates a transcript application for a customer
    /// requesting transcripts
    /// </summary>
    /// <param name="createApplicationRequest"></param>
    /// <returns>
    /// The Id of the application
    /// </returns>
    [AllowAnonymous]
    [HttpPost("Create-transcript-application", Name = "Create-transcript-application")]
    [SwaggerOperation(Summary = "Creates a transcript application")]
    /* [SwaggerResponse(StatusCodes.Status200OK, Description = "returns the application Id", Type = typeof(AuthenticationResponse))]
     [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create transcript application", Type = typeof(ErrorResponse))]
     [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]*/
    public async Task<ActionResult<CreateApplicationResponseDto>> CreateTranscriptApplication([FromBody] CreateApplicationDto createApplicationRequest)
    {
        var response = await _transcriptApplicationService.CreateApplication(createApplicationRequest);
        return Ok(response);
    }

    /* [AllowAnonymous]
     [HttpGet("get-applications", Name = "get-applications")]
     [SwaggerOperation(Summary = "Admin gets applications")]*/
    /*[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<TranscriptApplicationResponse>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "", Type = typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]*/
    //  public async Task<IActionResult> GetAllApplications([FromQuery] TranscriptAppParameter transcriptAppParameter)
    /*{
        var pagedResult = await _transcriptApplicationService.GetAllTranscriptApplication(transcriptAppParameter);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.transcriptApplications); ;
    }*/
}
