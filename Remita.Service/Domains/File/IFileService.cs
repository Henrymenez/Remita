using Remita.Services.Domains.File.Dtos;

namespace Remita.Services.Domains.File;

public interface IFileService
{
    public Task<FileUploadResponse> UploadFileAsync(UploadRequest request);
}
