using Microsoft.AspNetCore.Mvc;
using Remita.Services.Domains.File.Dtos;
using Remita.Services.Domains.File;
using Remita.Controllers.v1.Shared;

namespace Remita.Api.Controllers.v1;


[Route("api/v{version:apiVersion}/file")]
[ApiVersion("1.0")]
[ApiController]
public class FileController : BaseController
{
    private readonly IFileService _fileService;
    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("upload-file", Name = "upload-file")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    public async Task<IActionResult> Upload([FromForm] UploadRequest request)
    {
        var fileUploadSummary = await _fileService.UploadFileAsync(request);
        return CreatedAtAction(nameof(Upload), fileUploadSummary);
    }
}
