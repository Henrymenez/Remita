using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.File.Dtos;

public record UploadRequest
{
    [Required]
    public string Course { get; set; }
    [Required]
    public int Credit { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public string AcademicYear { get; set; }
    [Required]
    public string Semester { get; set; }

    [Required]
    public IFormFile File { get; set; }
}
