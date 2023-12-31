﻿using Remita.Services.Domains.ApplicationFee.Dtos;

namespace Remita.Services.Domains.ApplicationFee;

public interface IApplicationFeeService
{
    Task<ApplicationFeeResponseDto> CreateApplicationFee(CreateApplicationFeeDto applicationFeeDto);
    Task<ApplicationFeeResponseDto> UpdateApplicationFee(string id, CreateApplicationFeeDto applicationFeeDto);
   // Task<IEnumerable<TranscriptApplicationFee>> RetrieveApplicationFees(Pagination pageingOptions);
}
