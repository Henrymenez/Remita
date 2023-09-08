using Remita.Services.Domains.Transcript.Dtos;

namespace Remita.Services.Domains.Transcript;

public interface ITranscriptService
{
    Task<CreateApplicationResponseDto> CreateApplication(CreateApplicationDto applicationRequestDto);
    Task<TranscriptResponse> UpdateApplicationStatus(TranscriptRequestDto request, string Id);

    Task<bool> GenerateFees();


 //   Task<(IEnumerable<TranscriptApplicationResponse> transcriptApplications, MetaData metaData)> GetAllTranscriptApplication(TranscriptAppParameter transcriptAppParameter);

}
