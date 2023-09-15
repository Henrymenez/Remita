using Remita.Services.Domains.Results.Dtos;

namespace Remita.Services.Domains.Results;

public interface IResultService
{
    public Task<ResultResponse> ApproveResult(ResultRequest request);
}
