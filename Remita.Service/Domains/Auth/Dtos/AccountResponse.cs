namespace Remita.Services.Domains.Auth.Dtos
{
    public record AccountResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
