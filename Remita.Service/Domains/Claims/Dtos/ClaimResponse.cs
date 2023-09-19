namespace Remita.Services.Domains.Claims.Dtos
{
    public class ClaimResponse
    {
        public string Role { get; set; } = null!;
        public string ClaimType { get; set; } = null!;
    }

    public class ClaimRequest
    {
        public string Role { get; set; } = null!;
        public string ClaimType { get; set; } = null!;
    }
}
