using Remita.Models.Domains.ApplicationFee.Enum;

namespace Remita.Services.Domains.ApplicationFee.Dtos;

public record CreateApplicationFeeDto
{
    public DeliveryFormat DeliveryFormat { get; set; }
    public decimal ApplicationFee { get; set; }
}
