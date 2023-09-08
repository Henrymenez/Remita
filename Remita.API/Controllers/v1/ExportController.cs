using Microsoft.AspNetCore.Mvc;
using Remita.Services.Domains.FileExport;
using Swashbuckle.AspNetCore.Annotations;

namespace Remita.Api.Controllers.v1;

[Route("api/v{version:apiVersion}/export")]
[ApiVersion("1.0")]
[ApiController]
public class ExportController : Controller
{
    private IExportService _dataexport;

    public ExportController(IExportService dataexport)
    {
        _dataexport = dataexport;
    }


    [HttpPost("export", Name = "Export-Result")]
    [SwaggerOperation(Summary = "Export Student Result")]
    /*[SwaggerResponse(StatusCodes.Status200OK, Description = "Data Exported Successfully", Type = typeof(ExportResponse))]
    [SwaggerResponse(StatusCodes.Status204NoContent, Description = "User has no Course registered", Type = typeof(ExportResponse))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ExportResponse))]*/
    public async Task<IActionResult> ExportResult(string email)
    {
        var response = await _dataexport.ExportData(email);
        if (response.success)
            return Ok(response);

        return BadRequest(response);
    }
}
