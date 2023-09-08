using Remita.Services.Domains.Transcript.Dtos;

namespace Remita.Services.Domains.Transcript;

public class TranscriptService : ITranscriptService
{
    public TranscriptService()
    {

    }

    public Task<CreateApplicationResponseDto> CreateApplication(CreateApplicationDto applicationRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GenerateFees()
    {
        throw new NotImplementedException();
    }

    public Task<TranscriptResponse> UpdateApplicationStatus(TranscriptRequestDto request, string Id)
    {
        throw new NotImplementedException();
    }
}
