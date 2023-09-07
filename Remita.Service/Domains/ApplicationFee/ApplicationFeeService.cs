using Remita.Services.Domains.ApplicationFee.Dtos;

namespace Remita.Services.Domains.ApplicationFee;

public class ApplicationFeeService : IApplicationFeeService
{
    public ApplicationFeeService()
    {

    }
    public Task<ApplicationFeeResponseDto> CreateApplicationFee(CreateApplicationFeeDto applicationFeeDto)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationFeeResponseDto> UpdateApplicationFee(string id, CreateApplicationFeeDto applicationFeeDto)
    {
        throw new NotImplementedException();
    }

}
