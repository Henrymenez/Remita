using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.File.Dtos;

public record UploadRequest
{
    [Required]
    public string Course { get; set; } = null!;
    [Required]
    public int Credit { get; set; }
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string AcademicYear { get; set; } = null!;
    [Required]
    public string Semester { get; set; } = null!;

    [Required]
    public IFormFile File { get; set; } = null!;
}
