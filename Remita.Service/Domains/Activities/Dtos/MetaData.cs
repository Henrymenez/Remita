namespace Remita.Services.Domains.Activities.Dtos;

public record MetaData
{
    public string UserId { get; set; } = null!;
    public DateTime TimeDate { get; set; } = DateTime.UtcNow;
}
