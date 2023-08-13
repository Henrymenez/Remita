using System.ComponentModel.DataAnnotations;

namespace Remita.Services.Domains.Auth.Dtos;

/// <summary>
/// Client request payload for the creation of new accounts
/// </summary>
/// <param name="Email"></param>
/// <param name="Password"></param>
/// <param name="ConfirmPassword"></param>
public record SignUpDto([Required] string Email, [Required] string Password, [Required] string ConfirmPassword);
