using Remita.Services.Domains.Results.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remita.Services.Domains.Results;

public class ResultService : IResultService
{
	public ResultService()
	{

	}

    public Task<ResultResponse> ApproveResult(ResultRequest request)
    {
        throw new NotImplementedException();
    }
}
