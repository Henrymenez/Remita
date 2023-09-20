namespace Remita.Services.Domains.Activities.Dtos;

public record SignUpActivity
{
    public bool IsSuccessful { get; set; }
    public MetaData? UserData { get; set; }
}
